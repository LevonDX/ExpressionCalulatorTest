using ExpressionCalulatorTest;

namespace ExpressionCalculatorTest.Tests
{
    public class TestExpressionCalculator
    {
        [Fact]
        public void TestGetMethodName()
        {
            // 1. Arrange 
            string correctMethod = "Pow(2, 3)";
            string incorrectMethod = "Ararat73";
            string emptyMethod = "";

            MathExpressionSolver correct = new MathExpressionSolver(correctMethod);
            MathExpressionSolver incorrect = new MathExpressionSolver(incorrectMethod);
            MathExpressionSolver empty = new MathExpressionSolver(emptyMethod);

            // 2. Act
            string correctMethodName = correct.GetMethodName();

            // 3. Assert
            Assert.Equal("Pow", correctMethodName);

            Assert.Throws<ArgumentException>(() => incorrect.GetMethodName());
            Assert.Throws<ArgumentException>(() => empty.GetMethodName());

        }

        [Fact]
        public void TestGetArguments()
        {
            // 1. Arrange
            string singleArgumentExpression = "func(3.5)";
            string twoArgumentExpression = "func(1.1,2.2)";
            string invalidFormatExpression = "func1.1,2.2";
            string invalidParenthesesExpression = "func)1.1,2.2(";
            string tooManyArgumentsExpression = "func(1.1,2.2,3.3)";
            string nonNumericArgumentExpression = "func(abc)";
            string emptyArgumentsExpression = "func()";

            MathExpressionSolver singleArgumentSolver = new MathExpressionSolver(singleArgumentExpression);
            MathExpressionSolver twoArgumentSolver = new MathExpressionSolver(twoArgumentExpression);
            MathExpressionSolver invalidFormatSolver = new MathExpressionSolver(invalidFormatExpression);
            MathExpressionSolver invalidParenthesesSolver = new MathExpressionSolver(invalidParenthesesExpression);
            MathExpressionSolver tooManyArgumentsSolver = new MathExpressionSolver(tooManyArgumentsExpression);
            MathExpressionSolver nonNumericArgumentSolver = new MathExpressionSolver(nonNumericArgumentExpression);
            MathExpressionSolver emptyArgumentsSolver = new MathExpressionSolver(emptyArgumentsExpression);

            // 2. Act
            double[] singleArgumentResult = singleArgumentSolver.GetArguments();
            double[] twoArgumentResult = twoArgumentSolver.GetArguments();

            // 3. Assert

            // Valid cases
            Assert.Single(singleArgumentResult);
            Assert.Equal(3.5, singleArgumentResult[0]);

            Assert.Equal(2, twoArgumentResult.Length);
            Assert.Equal(1.1, twoArgumentResult[0]);
            Assert.Equal(2.2, twoArgumentResult[1]);

            // Invalid cases
            Assert.Throws<ArgumentException>(() => invalidFormatSolver.GetArguments());
            Assert.Throws<ArgumentException>(() => invalidParenthesesSolver.GetArguments());
            Assert.Throws<ArgumentException>(() => tooManyArgumentsSolver.GetArguments());
            Assert.Throws<ArgumentException>(() => nonNumericArgumentSolver.GetArguments());
            Assert.Throws<ArgumentException>(() => emptyArgumentsSolver.GetArguments());
        }
    }
}