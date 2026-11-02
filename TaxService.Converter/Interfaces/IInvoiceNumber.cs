namespace TaxService.Manager.Interfaces
{
    public interface IInvoiceNumber
    {
        string Number { get; set; }
        string Series { get; set; }
    }
}