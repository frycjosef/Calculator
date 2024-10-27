namespace ASTRATEXCalculator.Common.Models;

public class ItemsContainer<T>
{
    public ItemsContainer()
    {

    }

    public ItemsContainer(IEnumerable<T> items, long total)
    {
        Total = total;
        Items = items;
    }

    public long? Total { get; set; }
    public IEnumerable<T> Items { get; set; }

}