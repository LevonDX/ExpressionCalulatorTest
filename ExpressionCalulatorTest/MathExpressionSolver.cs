using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionCalulatorTest
{
    public class MathExpressionSolver
    {
        private readonly string _expression;

        public string Expression { get { return _expression; } }

        public MathExpressionSolver(string expression)
        {
            _expression = expression;
        }

        /// <summary>
        /// Returns name of method from string. E.g. pow(2, 3) returns pow
        /// </summary>
        /// <returns>name of method</returns>
        /// <exception cref="ArgumentException"></exception>
        public string GetMethodName()
        {
            int bracketIndex = _expression.IndexOf('(');

            if (bracketIndex == -1)
                throw new ArgumentException("Invalid format");

            string methodName = _expression.Substring(0, bracketIndex);

            return methodName;
        }

        /// <summary>
        /// returns arguments of method. E.g. pow(2, 3) return double[] {2, 3}
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double[] GetArguments()
        {
            int openBracketIndex = _expression.IndexOf("(");
            int closeBracketIndex = _expression.IndexOf(")");

            if (openBracketIndex == -1 || closeBracketIndex == -1 ||
                closeBracketIndex < openBracketIndex)
                throw new ArgumentException("Invalid format");

            string argString = _expression.Substring(openBracketIndex + 1,
                closeBracketIndex - openBracketIndex - 1);

            string[] args = argString.Split(new char[] { ',' });

            if (!(args.Length == 1 || args.Length == 2))
                throw new ArgumentException("Invalid arguments");

            double[] doubleArgs = new double[args.Length];

            for (int i = 0; i < args.Length; i++)
            {
                if (!double.TryParse(args[i], out doubleArgs[i]))
                {
                    throw new ArgumentException("Invalid arguments");
                }
            }

            return doubleArgs;
        }

        
        public MathMethodInfo GetMethodInfo()
        {
            string methodName = GetMethodName(); // get name of method
            double[] args = GetArguments(); // get arguments of method

            foreach (MethodInfo mi in typeof(Math).GetMethods())
            {
                // check if in Math class there is a method with the given name and number of arguments
                if (mi.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase) &&
                    mi.GetParameters().Length == args.Length)

                    return new MathMethodInfo(mi, args, methodName); // method found
            }

            return new MathMethodInfo(null, args, methodName); // method not found, so return MathMethodInfo with null MethodInfo
        }

        public double Calculate()
        {
            MathMethodInfo method = GetMethodInfo();

            if (method.MethodInfo == null)
                throw new InvalidOperationException($"Invalid method name {method.MethodName}");

            double[] args = method.Args;

            MethodInfo methodInfo = method.MethodInfo;

            object[] objArgs = new object[args.Length];

            int i = 0;
            foreach (double item in args)
            {
                objArgs[i++] = item;
            }

            object? objResult = methodInfo.Invoke(null, objArgs);

            // if return value is not double, throw exception, otherwise return result
            double result = objResult as double? ?? throw new InvalidOperationException("Return value is not double");

            return result;
        }
    }
}