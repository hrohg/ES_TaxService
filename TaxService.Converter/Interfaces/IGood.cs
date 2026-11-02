namespace TaxService.Manager.Interfaces
{
    public interface IGood //: ITotal
    {
        string Description { get; set; }
        string Unit { get; set; }
        decimal Amount { get; set; }
        decimal PricePerUnit { get; set; }
        decimal Price { get; set; }
    }
}