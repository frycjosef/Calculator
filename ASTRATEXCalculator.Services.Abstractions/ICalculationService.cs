using ASTRATEXCalculator.Common.Dtos;
using ASTRATEXCalculator.Common.Models;

namespace ASTRATEXCalculator.Services.Abstractions;

public interface ICalculationService
{
    void AddCalculation(Calculation calculationDto);
    ItemsContainer<Calculation> GetRecentCalculations();
}