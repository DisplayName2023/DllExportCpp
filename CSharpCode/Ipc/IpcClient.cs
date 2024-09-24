using System;
using System.IO;
using System.IO.Pipes;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace IPCClient
{
    class Program
    {
        private const string PipeName = "FunctionPipe";
        private const string SharedMemoryName = "FunctionSharedMemory";
        private const string MutexName = "FunctionMutex";

        static void Main(string[] args)
        {
            using (var pipeClient = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut))
            using (var sharedMemory = MemoryMappedFile.OpenExisting(SharedMemoryName))
            using (var mutex = Mutex.OpenExisting(MutexName))
            {
                pipeClient.Connect();

                using (var writer = new StreamWriter(pipeClient))
                using (var reader = new StreamReader(pipeClient))
                {
                    var functionName = "Add"; // or "Concatenate"
                    var parameters = new object[] { 5, 3 }; // or new object[] { "Hello, ", "world!" }

                    writer.WriteLine(functionName);
                    writer.Flush();

                    mutex.WaitOne();

                    try
                    {
                        using (var accessor = sharedMemory.CreateViewStream())
                        {
                            var formatter = new BinaryFormatter();
                            formatter.Serialize(accessor, parameters);
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }

                    var response = reader.ReadLine();
                    Console.WriteLine(response);

                    mutex.WaitOne();

                    try
                    {
                        using (var accessor = sharedMemory.CreateViewStream())
                        {
                            var formatter = new BinaryFormatter();
                            var result = formatter.Deserialize(accessor);
                            Console.WriteLine($"Result: {result}");
                        }
                    }
                    finally
                    {
                        mutex.ReleaseMutex();
                    }
                }
            }
        }
    }
}