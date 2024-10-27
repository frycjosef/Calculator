namespace ASTRATEXCalculator.Data.Repositories.Interfaces;

public interface IRepositoryManager : IDisposable
{
    public ICalculationsRepository Calculations { get; }
    
    void Attach<T>(T entity) where T : class;
  
    void Delete<T>(T obj) where T : class;
    void Add<T>(T obj) where T : class;
  
    void SaveChanges();
}