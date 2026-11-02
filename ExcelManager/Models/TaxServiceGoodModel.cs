using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TaxServiceInvoices.Models
{
    [Serializable]
    public class GoodInfos : INotifyPropertyChanged
    {
        public GoodInfos()
        {
        }
        #region Invoice items model properties
        private const string CodeProperties = "Code";
        private const string DescriptionProperties = "Description";
        private const string UnitProperties = "Unit";
        private const string AmountProperty = "Amount";
        private const string DiscountProperties = "Discount";
        private const string PricePerUnitProerties = "PricePerUnit";
        private const string PriceProerties = "Price";
        private const string TotalPriceProperty = "TotalPrice";
        private const string NoteProperties = "Note";
        #endregion
        #region Invoice items model private properties
        private string _code;
        private string _description;
        private string _unit;
        private string _note;
        private decimal? _amount;
        private decimal? _discount;
        private decimal? _pricePerUnit;
        private decimal? _price;
        #endregion
        #region Invoice items public properties
        [XmlIgnore]
        public string Code { get { return _code; } set { _code = value; OnPropertyChanged(CodeProperties); } }
        [XmlElement]
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(DescriptionProperties); } }
        [XmlElement]
        public string Unit { get { return _unit; } set { _unit = value; OnPropertyChanged(UnitProperties); } }
        [XmlElement]
        public decimal? Amount { get { return _amount; } set { _amount = value; OnPropertyChanged(AmountProperty); }}
        [XmlIgnore]
        public decimal? Discount { get { return _discount; } set { _discount = value; OnPropertyChanged(DiscountProperties); OnPropertyChanged(PriceProerties); } }
        [XmlElement]
        public decimal? PricePerUnit { get { return _pricePerUnit; } set { _pricePerUnit = value; OnPropertyChanged(PricePerUnitProerties); OnPropertyChanged(PriceProerties); } }
        public decimal? Price { get { return _price; } set { _price = value; OnPropertyChanged(PriceProerties); OnPropertyChanged(TotalPriceProperty); } }
        [XmlElement]
        public decimal TotalPrice { get { return _amount != null && Price != null ? (decimal) Price*(decimal) Price:0; } set{}}
        [XmlIgnore]
        public string Note { get { return _note; } set { _note = value; OnPropertyChanged(NoteProperties); } }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
