namespace CashReg.Models
{
    public class RequestGetOperatorsAndDeps
    {
        public string password { get; private set; }
        public RequestGetOperatorsAndDeps(string pas)
        {
            password = pas;
        }
    }
}
