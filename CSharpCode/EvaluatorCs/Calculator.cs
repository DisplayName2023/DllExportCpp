using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;

namespace EvaluatorCs
{


    public class Calculator
    {


        protected MethodInfo? ᜁm_method;

        protected Func<double, double, double>? m_funcᜂ;

        internal static Dictionary<string, Calculator> m_ᜀdict = new Dictionary<string, Calculator>();
        public static Dictionary<string, Calculator> Dict => m_ᜀdict;

        public Calculator()
        {

        }

        public Calculator(string function)
        {
            m_funcᜂ = CreateFunction(function);
        }

        public virtual double? Calculate(string expression, double x, double y)
        {
            try
            {
                var inst = Create(expression);
                return inst.DoCalculate(expression, x, y);
            }
            catch
            {
                
            }
            return null;
        }
        public virtual double? DoCalculate(string expression, double x, double y)
        {

            if (m_funcᜂ != null)
            {
                return m_funcᜂ(x, y);
            }
            return null;
        }



        public static Calculator Create(string function)
        {
            if (!Calculator.m_ᜀdict.TryGetValue(function, out var value))
            {
                value = new Calculator(function);
                if (value == null)
                {
                    throw new InvalidOperationException();
                }
                Calculator.m_ᜀdict.Add(function, value);
            }
            return value;
        }


        private static readonly Dictionary<string, string> MathFunctionMappings = new Dictionary<string, string>
            {
                { "sqrt", "Math.Sqrt" },
                { "sin", "Math.Sin" },
                { "cos", "Math.Cos" },
                { "tan", "Math.Tan" },
                { "abs", "Math.Abs" },
                { "pi", "Math.PI" },
                { "pow", "Math.Pow" },
                { "log", "Math.Log" },
                { "log10", "Math.Log10" },
                { "exp", "Math.Exp" },
                { "asin", "Math.Asin" },
                { "acos", "Math.Acos" },
                { "atan", "Math.Atan" },
                { "sinh", "Math.Sinh" },
                { "cosh", "Math.Cosh" },
                { "tanh", "Math.Tanh" },
                { "floor", "Math.Floor" },
                { "ceil", "Math.Ceiling" },
                { "round", "Math.Round" },
                //{ "e", "Math.E" },
            };

        public static Func<double, double, double> CreateFunction(string function)
        {
            foreach (var mapping in MathFunctionMappings)
            {
                function = function.Replace(mapping.Key, mapping.Value);
            }

            string script = $@"
            using System;
            using System.Reflection;

            public static class UserFunctions
            {{
                public static double Function(double x, double y)
                {{
                    return {function};
                }}
            }}
            
            var method = typeof(UserFunctions).GetMethod(""Function"", BindingFlags.Public | BindingFlags.Static);
            return method;

            ";

            


            var options = ScriptOptions.Default.WithImports("System"); //.AddReferences(AppDomain.CurrentDomain.GetAssemblies());
            
            var scriptState = CSharpScript.RunAsync(script, options).Result;

            var method = scriptState.ReturnValue as MethodInfo;

            return (Func<double, double, double>)Delegate.CreateDelegate(typeof(Func<double, double, double>), method);
        }


    }

}