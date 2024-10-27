using System;
using ASTRATEXCalculator.Common.Dtos;
using ASTRATEXCalculator.Common.Enums;
using NLog;

namespace ASTRATEXCalculator.CalculatorLibrary
{
    public class Calculator
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public bool ReturnIntegers { get; set; } = false;
        private const int ADDITIONAL_DECIMAL_PLACES = 2;

        public delegate void ErrorHandler(Exception exception);
        public event ErrorHandler SendError;

        public Calculation Calculate(Calculation calculation)
        {
            try
            {
                Logger.Info($"Starting calculation: {calculation.FirstNumber} {GetOperandSymbol(calculation.Operation)} {calculation.SecondNumber}");

                double firstNumber = double.Parse(calculation.FirstNumber);
                double secondNumber = double.Parse(calculation.SecondNumber);
                double result = 0;
                
                if (double.IsInfinity(firstNumber) || double.IsInfinity(secondNumber))
                {
                    throw new OverflowException("The number is too large to be represented as a double.");
                }

                switch (calculation.Operation)
                {
                    case Operation.Addition:
                        result = firstNumber + secondNumber;
                        break;
                    case Operation.Subtraction:
                        result = firstNumber - secondNumber;
                        break;
                    case Operation.Multiplication:
                        result = firstNumber * secondNumber;
                        break;
                    case Operation.Division:
                        if (secondNumber == 0)
                            throw new DivideByZeroException("Nelze dělit nulou.");
                        result = firstNumber / secondNumber;
                        break;
                    default:
                        throw new InvalidOperationException("Neplatná operace.");
                }
        
                if (ReturnIntegers)
                {
                    result = Math.Round(result);
                }
                else
                {
                    // Determine the number of decimal places in the inputs
                    int decimalPlacesInFirst = GetDecimalPlaces(calculation.FirstNumber);
                    int decimalPlacesInSecond = GetDecimalPlaces(calculation.SecondNumber);
                    
                    int maxDecimalPlaces = Math.Max(decimalPlacesInFirst, decimalPlacesInSecond);
                    int roundingPrecision = maxDecimalPlaces + ADDITIONAL_DECIMAL_PLACES;
        
                    result = Math.Round(result, roundingPrecision); // Round to the calculated number of decimal places
                }

                calculation.Result = result.ToString();
                calculation.Created = DateTime.Now;

                Logger.Info($"Calculation successful: Result = {calculation.Result}");
                return calculation;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred during calculation");
                SendError?.Invoke(ex);
                throw;
            }
        }

        private int GetDecimalPlaces(string number)
        {
            if (number.Contains(',') || number.Contains('.'))
            {
                return number.Split(',', '.')[1].Length;
            }
            return 0;
        }

        private string GetOperandSymbol(Operation operation)
        {
            return operation switch
            {
                Operation.Addition => "+",
                Operation.Subtraction => "−",
                Operation.Multiplication => "×",
                Operation.Division => "÷",
                _ => "?"
            };
        }
    }
}
