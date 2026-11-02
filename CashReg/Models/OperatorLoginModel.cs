using CashReg.Helper;
using CashReg.Interfaces;

namespace CashReg.Models
{
    public class OperatorLoginModel
    {
        public class RequestOperatorLogin
        {
            #region Internal fields
            private string _password;
            private int _cashier;
            private string _pin;
            #endregion

            #region External properties
            public string password { get { return _password; } set { _password = value; } }
            public int cashier { get { return _cashier; } set { _cashier = value; } }
            public string pin { get { return _pin; } set { _pin = value; } }
            #endregion

            #region Constructors
            public RequestOperatorLogin(IEcrCashier ecrCashier, string pass)
            {
                if (ecrCashier != null)
                {
                    cashier = ecrCashier.Cashier;
                    pin = ecrCashier.Pin;
                }
                password = pass;
            }
            #endregion
        }

        public class ResponseOperatorLogin
        {
            #region Internal fields
            private string _key;
            #endregion

            #region External fields
            public int ResponseCode { get; set; }
            public string Key { get { return _key; } set { _key = value; } }
            #endregion

            #region External methods
            public byte[] GetKeyBytes()
            {
                return System.Convert.FromBase64String(_key);
            }
            #endregion
        }

        public class RequestLogin
        {
            private readonly string _password;
            private readonly EcrCashier _cashier;
            public string Password { get { return _password; } }
            public int Cashier { get { return _cashier.Cashier; } }
            public string Pin { get { return _cashier.Pin; } }

            public RequestLogin(EcrCashier ecrCasier, string password)
            {
                _cashier = ecrCasier ?? new EcrCashier();
                _password = password;
            }
        }
    }
}
