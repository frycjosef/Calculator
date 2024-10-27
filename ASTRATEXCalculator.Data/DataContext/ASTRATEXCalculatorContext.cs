using ASTRATEXCalculator.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASTRATEXCalculator.Data.DataContext;

public class ASTRATEXCalculatorContext : DbContext
{
    public ASTRATEXCalculatorContext(DbContextOptions<ASTRATEXCalculatorContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Calculation> Calculations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculation>().ToTable("Calculations");
    }
}