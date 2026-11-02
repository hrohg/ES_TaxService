using System.Collections.Generic;
using CashReg.Helper;

namespace CashReg.Models
{
    public class RequestReceiptHeader
    {
        private readonly int _seq;
        private readonly List<ReceiptHeader> _headers = new List<ReceiptHeader>();
        private readonly List<ReceiptHeader> _footers = new List<ReceiptHeader>();
        public int seq { get { return _seq; } }
        public List<ReceiptHeader> headers { get { return _headers; } }
        public List<ReceiptHeader> footers { get { return _footers; } }
        public void AddHeader(ReceiptHeader item)
        { _headers.Add(item); }
        public void AddFooter(ReceiptHeader item)
        { _footers.Add(item); }
        public RequestReceiptHeader(int value)
        {
            _seq = value;
        }
    }
}
