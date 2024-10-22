namespace ExpressionCalulatorTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Write("\nExpression = ");
                string? str = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(str))
                    continue;

                try
                {
                    MathExpressionSolver solver = new MathExpressionSolver(str);
                    double result = solver.Calculate();

                    Console.WriteLine($"result = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

            } while (true);
        }
    }
}
