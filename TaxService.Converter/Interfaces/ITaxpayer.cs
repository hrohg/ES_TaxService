using TaxService.Manager.Models;

namespace TaxService.Manager.Interfaces
{
    public interface ITaxpayer
    {
        string TIN { get; set; }
        string Name { get; set; }
        string Address { get; set; }
    }

    public interface ISupplierTaxpayer : ITaxpayer
    {
        BankAccount BankAccount { get; set; }
    }
    public interface IBuyerTaxpayer : ITaxpayer
    {
        bool TinNotRequired { get; set; }
    }
}