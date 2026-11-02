using TaxService.Manager.Models;

namespace TaxService.Manager.Interfaces
{
    public interface IBuyerInfo
    {
        string VATNumber { get; set; }
        BuyerTaxpayer Taxpayer { get; set; }
        string DeliveryLocation { get; set; }
    }
}