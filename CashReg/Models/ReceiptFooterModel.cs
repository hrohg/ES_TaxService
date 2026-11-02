using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashReg.Models
{
    public class RequestReceiptHeaderLogo
    {
        private readonly int _seq;
        private string _headerLogo;


        public int seq { get { return _seq; } }
        public string headerLogo { get { return _headerLogo; } set { _headerLogo = value; } }
        public RequestReceiptHeaderLogo(int value)
        {
            _seq = value;
        }
        public RequestReceiptHeaderLogo(int value, string logo)
        {
            _seq = value;
            headerLogo = logo;
        }
    }
}
