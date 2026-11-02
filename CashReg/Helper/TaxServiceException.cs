using System;

namespace CashReg.Helper
{
    public class TaxServiceException : Exception
    {
        public TaxServiceException(string message):base(message)
        {
            
        }
    }
}
