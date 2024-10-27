using ASTRATEXCalculator.Data.DataContext;
using ASTRATEXCalculator.Data.Repositories.Interfaces;

namespace ASTRATEXCalculator.Data.Repositories;

public class RepositoryManager(ASTRATEXCalculatorContext calculatorContext) : IRepositoryManager
{
    private readonly ICalculationsRepository _calculations = new CalculationsRepository(calculatorContext);

    
    public ICalculationsRepository Calculations => _calculations;
    
    
    public void Dispose()
    {
        calculatorContext.Dispose();
    }

    public void Attach<T>(T entity) where T : class
    {
        calculatorContext.Set<T>().Attach(entity);
    }

    public void Delete<T>(T obj) where T : class
    {
        calculatorContext.Set<T>().Remove(obj);
    }

    public void Add<T>(T obj) where T : class
    {
        calculatorContext.Set<T>().Add(obj);
    }

    public void SaveChanges()
    {
        calculatorContext.SaveChanges();
    }
}