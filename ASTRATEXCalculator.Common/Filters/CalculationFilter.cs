using ASTRATEXCalculator.Common.Enums;

namespace ASTRATEXCalculator.Common.Filters;

public class CalculationFilter : PagingFilter
{
    public EntityStatus? Status { get; set; }
}