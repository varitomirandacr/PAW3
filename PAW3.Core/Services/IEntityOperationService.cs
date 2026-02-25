namespace PAW3.Core.Services;

public interface IEntityOperationService
{
    decimal Average(decimal[] values);
    string RemoveSpaces(string input);
    IEnumerable<decimal> SumEach(List<decimal> values, decimal num);
}

public class EntityOperationService : IEntityOperationService
{
    public decimal Average(decimal[] values)
    {
        return values.Length == 0 ? 0 : values.Average();
        //.Sum() / values.Length;
    }

    public IEnumerable<decimal> SumEach(List<decimal> values, decimal num)
    {
        var results = new List<decimal>();
        values.ForEach(x =>
        {
            results.Add(x + num);
        });

        return results;
    }

    public string RemoveSpaces(string input)
    {
        return input.Replace(" ", string.Empty);
    }
}
