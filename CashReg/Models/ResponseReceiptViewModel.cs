using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashReg.Models
{
    public class ResponseReceiptViewModel
    {
        public ResponseReceiptModel ResponseReceipt { get; set; }

        public ResponseReceiptViewModel(ResponseReceiptModel responseReceipt)
        {
            ResponseReceipt = responseReceipt;
        }
    }
}
