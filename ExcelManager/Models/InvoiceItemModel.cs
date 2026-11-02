using System;
using System.ComponentModel;

namespace TaxServiceInvoices.Models
{
    public class InvoiceItemsModel : INotifyPropertyChanged
    {
        public InvoiceItemsModel(InvoiceModel invoice)
        {
            _invoiceId = invoice.Id;
        }
        public InvoiceItemsModel(InvoiceItemsModel item)
        {
            _id = item.Id;
            _invoiceId = item.InvoiceId;
            _productId = item.ProductId;
           _productItemId = item.ProductItemId;
            _code = item.Code;
            _description = item.Description;
            _mu = item.Mu;
            _note = item.Note;
            _quantity = item.Quantity;
            _price = item.Price;
            _discount = item.Discount;
        }

        public InvoiceItemsModel()
        {
        }

        #region Invoice items model properties
        private const string IdProperties = "Id";
        private const string InvoiceIdProperties = "InvoiceId";
        private const string ProductIdProperties = "ProductId";
        private const string ProductItemIdProperties = "ProductItemId";
        private const string CodeProperties = "Code";
        private const string HcdCsProperties = "HcdCs";
        private const string DescriptionProperties = "Description";
        private const string MuProperties = "Mu";
        private const string QuantityProperties = "Quantity";
        private const string PriceProerties = "Price";
        private const string DiscountProperties = "Discount";
        private const string NoteProperties = "Note";
        private const string AmountProperty = "Amount";
        #endregion
        #region Invoice items model private properties
        private Guid _id = Guid.NewGuid();
        private Guid _invoiceId;
        private Guid _productId;
        private Guid? _productItemId;
        private string _code; 
        private string _hcdCs;
        private string _description;
        private string _mu;
        private string _note;
        private decimal? _quantity;
        private decimal? _price;
        private decimal? _discount;
        #endregion
        #region Invoice items public properties
        public Guid Id { get { return _id; } set { _id = value; } }
        public Guid InvoiceId { get { return _invoiceId; } set { _invoiceId = value; } }
        public Guid ProductId { get { return _productId; } set { _productId = value; } }
        public Guid? ProductItemId { get { return _productItemId; } set { _productItemId = value; } }
        public string Code { get { return _code; } set { _code = value; OnPropertyChanged(CodeProperties); } }
        public string HcdCs { get { return _hcdCs; } set { _hcdCs = value; OnPropertyChanged(HcdCsProperties); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(DescriptionProperties); } }
        public string Mu { get { return _mu; } set { _mu = value; OnPropertyChanged(MuProperties); } }
        public decimal? Quantity { get { return _quantity; } set { _quantity = value; OnPropertyChanged(QuantityProperties); OnPropertyChanged(AmountProperty); } }
        public decimal? Price { get { return _price; } set { _price = value; OnPropertyChanged(PriceProerties); OnPropertyChanged(AmountProperty); } }
        public decimal? CostPrice { get; set; }
        public decimal Amount { get { return Quantity != null && Price != null ? (decimal)Price * (decimal)Quantity * (1 + (Discount != null ? (decimal)Discount / 100 : 0)) : 0; } }
        public decimal? Discount { get { return _discount; } set { _discount = value; OnPropertyChanged(DiscountProperties); OnPropertyChanged(AmountProperty); } }
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
