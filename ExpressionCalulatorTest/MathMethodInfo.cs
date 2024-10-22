using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalulatorTest
{
    public class MathMethodInfo
    {
        public MethodInfo? MethodInfo { get; set; }  
        public double[] Args { get; set; } = new double[0];
        public string MethodName { get; set; } = "";

        public MathMethodInfo(MethodInfo? methodInfo, double[] args, string methodname)
        {
            MethodInfo = methodInfo;
            Args = args;
            MethodName = methodname;
        }
    }
}
