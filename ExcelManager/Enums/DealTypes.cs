using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaxServiceInvoices.Enums
{
    public enum DealTypes
    {
        [Display(Name = "SPECIAL_TAX_SYSTEM")]
        [Description("Հարկման հատուկ համակարգ")]
        SpecialTaxSystem = 1,

        [Display(Name = "VAT_EXEMPT")]
        [Description("ԱԱՀ-ից ազատված")]
        VatExempt = 2,

        [Display(Name = "VAT_ZERO_RATE")]
        [Description("ԱԱՀ-ի 0% դրույքաչափով հարկվող")]
        VatZeroRate = 3,
        
        [Display(Name = "EXCLUDED_FROM_VAT")]
        [Description("ԱԱՀ-ով հարկման օբյեկտ չհամարվող")]
        ExcludedFromVat = 4,
        
        [Display(Name = "EXPORT")]
        [Description("Արտահանում")]
        Export = 5,
        
        [Display(Name = "EXPORT_EAEU_AIR")]
        [Description("ԵՏՄ օդային տրանսպորտով արտահանում")]
        ExportEaeuAir = 6,
        
        [Display(Name = "VAT_NON_PAYERS")]
        [Description("ԱԱՀ չվճարողներ")]
        VatNonPayers = 7
    }
}
