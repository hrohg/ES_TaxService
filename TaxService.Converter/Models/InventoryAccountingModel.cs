using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TaxService.Manager.Models
{
    public class InventoryAccountingItemModel
    {
        [Description("Հ/Հ")]
        public string Sn { get; set; }

        [Description("Ապրանքի, արտադրանքի անվանումը")]
        public string ProductName { get; set; }

        [Description("Ապրանքի, արտադրանքի տեսակը")]
        public string ProductType { get; set; }

        [Description("Ապրանքի, արտադրանքի մոդելը")]
        public string ProductModel { get; set; }

        [Description("Չափման միավորը")]
        public string Mu { get; set; }

        [Description("Ապրանքի, արտադրանքի քանակը")]
        public string Quantity { get; set; }

        [Description("Միավորի իրացման գինը")]
        public string Price { get; set; }

        [Description("Ընդամենը գումարը")]
        public string TotalAmount { get; set; }

        [Description("Այլ նշումներ")]
        public string Notes { get; set; }
    }
    public class TaxpayerModel
    {
        public string Name { get; set; }
        public string Tin { get; set; }

    }
    public class InventoryAccountingModel
    {
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public TaxpayerModel TaxpayerModel { get; set; }
        public string Address { get; set; }
        public string StockKeeper { get; set; }
        public string MrpPosition { get; set; }
        public string Notes { get; set; }

        public List<InventoryAccountingItemModel> InventoryAccountingItemModel { get; set; }
    }
}
