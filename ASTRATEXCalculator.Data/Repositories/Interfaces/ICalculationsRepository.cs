using ASTRATEXCalculator.Common.Filters;
using ASTRATEXCalculator.Common.Models;
using ASTRATEXCalculator.Data.Entities;

namespace ASTRATEXCalculator.Data.Repositories.Interfaces;

public interface ICalculationsRepository : IBaseRepository
{
    public (IEnumerable<Calculation> calculations, long total) List(CalculationFilter filter);
}