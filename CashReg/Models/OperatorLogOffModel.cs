namespace CashReg.Models
{
    public class RequestLogoff
    {
        public int seq { get; private set; }

        public RequestLogoff(int seqValue)
        {
            seq = seqValue;
        }
    }
}
