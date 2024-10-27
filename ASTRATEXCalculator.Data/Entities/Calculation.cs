using ASTRATEXCalculator.Common.Enums;

namespace ASTRATEXCalculator.Data.Entities;

public class Calculation
{
    public long Id { get; set; }
    
    public string FirstNumber { get; set; }
    public Operation Operation { get; set; }
    public string SecondNumber { get; set; }
    public string Result { get; set; }
    public EntityStatus Status { get; set; }
    
    public DateTime Created { get; set; }
}