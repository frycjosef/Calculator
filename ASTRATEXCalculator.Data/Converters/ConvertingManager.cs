namespace ASTRATEXCalculator.Data.Converters;

public class ConvertingManager
{
    public ConvertingManager()
    {
        Calculations = new CalculationConverter(this);
    }
    
    public CalculationConverter Calculations { get; set; }
}