using ASTRATEXCalculator.Common.Dtos;

namespace ASTRATEXCalculator.Data.Converters;

public class CalculationConverter(ConvertingManager convertingManager)
{
    public Calculation EntityToDto(Entities.Calculation entity)
    {
        return new Calculation
        {
            FirstNumber = entity.FirstNumber,
            Operation = entity.Operation,
            SecondNumber = entity.SecondNumber,
            Result = entity.Result,
            Status = entity.Status,
            Created = entity.Created
        };
    }
}