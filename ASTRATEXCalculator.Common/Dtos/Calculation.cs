using System.ComponentModel.DataAnnotations;
using ASTRATEXCalculator.Common.Enums;

namespace ASTRATEXCalculator.Common.Dtos;

public class Calculation
{
    [Required(ErrorMessage = "First number is required")]
    public string FirstNumber { get; set; }
    [Required(ErrorMessage = "Operation is required")]
    public Operation Operation { get; set; }
    [Required(ErrorMessage = "Second number is required")]
    public string SecondNumber { get; set; }
    public string Result { get; set; }
    public EntityStatus Status { get; set; }
    
    public DateTime Created { get; set; }
}