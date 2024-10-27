using System.ComponentModel;

namespace ASTRATEXCalculator.Common.Enums;

public enum EntityStatus : short
{
    [Description("Aktivní")]
    Active = 0,
    [Description("Neaktivní")]
    Inactive = 1,
    [Description("Odstraněno")]
    Deleted = 1000
}