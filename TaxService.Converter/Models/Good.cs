using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using TaxService.Manager.Interfaces;

namespace TaxService.Manager.Models
{
    [Serializable]
    public class Total : ITotal
    {
        [XmlElement("TotalPrice", Order = 18)]
        public decimal TotalPrice { get; set; }
    }

    [Serializable]
    public class Good : IGood
    {
        [XmlElement(Order = 11)]
        public string Description { get; set; }

        [XmlElement(Order = 12)]
        public string ClassifierCode { get; set; } = string.Empty;

        [XmlElement(Order = 13)]
        public string Unit { get; set; }

        [XmlElement(Order = 14)]
        public decimal Amount { get; set; }

        [XmlElement(Order = 15)]
        public decimal PricePerUnit { get; set; }

        [XmlElement(Order = 16)]
        public decimal Price { get; set; }

        [XmlElement(Order = 17)]
        public string DealType { get; set; }

        [XmlElement("TotalPrice", Order = 18)]
        public decimal TotalPrice { get; set; }
    }

    [Serializable]
    public class GoodsInfo
    {
        [XmlElement("Good", typeof(Good))]
        public List<Good> Goods { get; set; } = new List<Good>();

        [XmlElement("Total")]
        public Total Total { get { return new Total { TotalPrice = Goods.Sum(s => s.TotalPrice) }; } set { } }
    }
}
