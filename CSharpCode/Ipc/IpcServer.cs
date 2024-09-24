
using System;
using System.IO;
using System.IO.Pipes;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Reflection;

namespace IPCServer
{
    class Program
    {
        private const string PipeName = "FunctionPipe";
        private const string SharedMemoryName = "FunctionSharedMemory";
        private const string MutexName = "FunctionMutex";

        static void Main(string[] args)
        {
            using (var pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut))
            using (var sharedMemory = MemoryMappedFile.CreateOrOpen(SharedMemoryName, 1024))
            using (var mutex = new Mutex(false, MutexName))
            {
                Console.WriteLine("Waiting for client connection...");
                pipeServer.WaitForConnection();

                while (true)
                {
                    using (var reader = new StreamReader(pipeServer))
                    using (var writer = new StreamWriter(pipeServer))
                    {
                        var functionName = reader.ReadLine();
                        if (functionName == null) break;

                        Console.WriteLine($"Received function call: {functionName}");

                        mutex.WaitOne();

                        try
                        {
                            using (var accessor = sharedMemory.CreateViewStream())
                            {
                                var formatter = new BinaryFormatter();
                                var parameters = (object[])formatter.Deserialize(accessor);

                                var result = InvokeFunction(functionName, parameters);

                                accessor.Position = 0;
                                formatter.Serialize(accessor, result);
                            }
                        }
                        finally
                        {
                            mutex.ReleaseMutex();
                        }

                        writer.WriteLine("Function executed");
                        writer.Flush();
                    }
                }
            }
        }

        private static object InvokeFunction(string functionName, object[] parameters)
        {
            var methods = typeof(Program).GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            foreach (var method in methods)
            {
                if (method.Name == functionName)
                {
                    return method.Invoke(null, parameters);
                }
            }

            throw new InvalidOperationException("Unknown function");
        }

        // Example functions
        private static int Add(int a, int b)
        {
            return a + b;
        }

        private static string Concatenate(string a, string b)
        {
            return a + b;
        }
    }
}