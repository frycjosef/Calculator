using ASTRATEXCalculator.Data.DataContext;
using ASTRATEXCalculator.Data.Repositories.Interfaces;

namespace ASTRATEXCalculator.Data.Repositories;

public class BaseRepository(ASTRATEXCalculatorContext context) : IBaseRepository, IDisposable
{
    public void Dispose()
    {
        this.Dispose();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void Attach<T>(T obj) where T : class
    {
        context.Set<T>().Attach(obj);
    }

    public void Delete<T>(T obj) where T : class
    {
        context.Set<T>().Remove(obj);
    }

    public void Add<T>(T obj) where T : class
    {
        context.Set<T>().Add(obj);
    }
}