using CashReg.Interfaces;

namespace CashReg.Models
{
    public class RequestPrintReceiptLatestCopy: IReceiptLatestCopy
    {
        /// <summary>
        /// Հարցման հերթական համարը
        /// </summary>
        public int seq { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seqValue"></param>
        public RequestPrintReceiptLatestCopy(int seqValue)
        {
            seq = seqValue;
        }
    }
}
