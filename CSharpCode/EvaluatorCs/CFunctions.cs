using System.Runtime.InteropServices;

namespace Evaluator
{
    public class CFunctions
    {
        public unsafe extern void Calculate(string expression, double x, double y, IntPtr output);
    }
}
