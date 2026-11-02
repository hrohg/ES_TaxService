using System.ComponentModel;

namespace CashReg.Enums
{
    public enum TypeOfTaxes
    {
        [Description("Անհայտ հարկման տեսակ")]
        None = 0,
        [Description("ԱԱՀ ով հարկվող")]
        WithVat = 1,
        [Description("ԱԱՀ ով չհարկվող")]
        WithoutVat = 2,
        [Description("Շրջանառության հարկ")]
        TurnoverTax = 3,
        [Description("Արտոնագրային վճար")]
        PatentFee = 4, // Այժմ չեն կիրառվում
        [Description("Հաստատագրված վճար")]
        FixedFee = 5, // Այժմ չեն կիրառվում
        [Description("Ընտանեկան ձեռնարկատիրություն")]
        FamilyBusiness = 6, // Այժմ չեն կիրառվում
        [Description("Միկրո ձեռնարկատիրություն")]
        MicroBusiness = 7
    }
}
