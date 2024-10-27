using ASTRATEXCalculator.Common.Dtos;
using ASTRATEXCalculator.Common.Enums;
using Xunit;

namespace ASTRATEXCalculator.CalculatorLibrary.Tests
{
    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [InlineData("3,5", "2,1", Operation.Addition, "5,6")]
        [InlineData("3", "2", Operation.Addition, "5")]
        [InlineData("5,5", "2,5", Operation.Subtraction, "3")]
        [InlineData("5", "2", Operation.Subtraction, "3")]
        [InlineData("3", "4", Operation.Multiplication, "12")]
        [InlineData("2,5", "2", Operation.Multiplication, "5")]
        [InlineData("10", "2", Operation.Division, "5")]
        [InlineData("10", "3", Operation.Division, "3,33")]
        public void Calculate_ValidOperations_ReturnsCorrectResult(string firstNumber, string secondNumber, Operation operation, string expectedResult)
        {
            // Arrange
            var calculation = new Calculation
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation
            };

            // Act
            var result = _calculator.Calculate(calculation);

            // Assert
            Assert.Equal(expectedResult, result.Result);
        }

        [Fact]
        public void Calculate_DivideByZero_ThrowsException()
        {
            // Arrange
            var calculation = new Calculation
            {
                FirstNumber = "10",
                SecondNumber = "0",
                Operation = Operation.Division
            };

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _calculator.Calculate(calculation));
        }

        [Theory]
        [InlineData("5,1234", "2", Operation.Multiplication, false, "10,2468")] // More decimal places in first number
        [InlineData("5", "2,6789", Operation.Multiplication, false, "13,3945")] // More decimal places in second number
        public void Calculate_RoundingPrecision_ReturnsRoundedResult(string firstNumber, string secondNumber, Operation operation, bool returnIntegers, string expectedResult)
        {
            // Arrange
            _calculator.ReturnIntegers = returnIntegers;
            var calculation = new Calculation
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation
            };

            // Act
            var result = _calculator.Calculate(calculation);

            // Assert
            Assert.Equal(expectedResult, result.Result);
        }

        [Theory]
        [InlineData("5,1234", "2", Operation.Multiplication, true, "10")] // Rounding as an integer
        [InlineData("5,5", "2", Operation.Multiplication, true, "11")] // Rounding as an integer
        public void Calculate_ReturnIntegers_RoundsToInteger(string firstNumber, string secondNumber, Operation operation, bool returnIntegers, string expectedResult)
        {
            // Arrange
            _calculator.ReturnIntegers = returnIntegers;
            var calculation = new Calculation
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation
            };

            // Act
            var result = _calculator.Calculate(calculation);

            // Assert
            Assert.Equal(expectedResult, result.Result);
        }
        
        [Theory]
        [InlineData("abc", "2", Operation.Addition)] // Invalid characters in the first number
        [InlineData("2", "xyz", Operation.Addition)] // Invalid characters in the second number
        [InlineData("1,2.3", "2", Operation.Addition)] // Mixed separators in the first number
        public void Calculate_InvalidCharacters_ThrowsFormatException(string firstNumber, string secondNumber, Operation operation)
        {
            // Arrange
            var calculation = new Calculation
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation
            };

            // Act & Assert
            Assert.Throws<FormatException>(() => _calculator.Calculate(calculation));
        }
        
        [Theory]
        [InlineData("1e500", "2", Operation.Addition)] // Too large number for double
        [InlineData("2", "1e500", Operation.Addition)] // Too large number for double
        public void Calculate_NumberTooLarge_ThrowsOverflowException(string firstNumber, string secondNumber, Operation operation)
        {
            // Arrange
            var calculation = new Calculation
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation
            };

            // Act & Assert
            Assert.Throws<OverflowException>(() => _calculator.Calculate(calculation));
        }
        
        [Theory]
        [InlineData(null, "2", Operation.Addition)]
        [InlineData("3", null, Operation.Addition)]
        [InlineData(null, null, Operation.Addition)]
        public void Calculate_NullInputs_ThrowsArgumentNullException(string firstNumber, string secondNumber, Operation operation)
        {
            // Arrange
            var calculation = new Calculation
            {
                FirstNumber = firstNumber,
                SecondNumber = secondNumber,
                Operation = operation
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _calculator.Calculate(calculation));
        }
    }
}
