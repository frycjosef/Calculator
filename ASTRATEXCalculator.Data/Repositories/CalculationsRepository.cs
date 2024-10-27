using ASTRATEXCalculator.Common.Filters;
using ASTRATEXCalculator.Common.Models;
using ASTRATEXCalculator.Data.DataContext;
using ASTRATEXCalculator.Data.Entities;
using ASTRATEXCalculator.Data.Repositories.Interfaces;

namespace ASTRATEXCalculator.Data.Repositories;

public class CalculationsRepository(ASTRATEXCalculatorContext context) : BaseRepository(context), ICalculationsRepository
{
    private IQueryable<Calculation> AllCalculations => context.Calculations;
    
    public (IEnumerable<Calculation> calculations, long total) List(CalculationFilter filter)
    {
        var calculations = AllCalculations;
        
        if(filter.Status.HasValue)
            calculations = calculations.Where(c => c.Status == filter.Status.Value);
        
        calculations = calculations.OrderByDescending(c => c.Created);
        
        var total = calculations.Count();
        
        if(filter.Skip.HasValue)
            calculations = calculations.Skip(filter.Skip.Value);
        
        if(filter.Take.HasValue)
            calculations = calculations.Take(filter.Take.Value);
        
        return (calculations, total);
    }
}