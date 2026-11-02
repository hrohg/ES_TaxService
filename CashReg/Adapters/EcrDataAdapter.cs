using CashReg.Helper;
using CashReg.Interfaces;
using System;

namespace CashReg.Adapters
{
    public class EcrDataAdapter : EcrAdapterBase
    {

        public EcrDataAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback) { }

        /// <summary>
        /// 4.5.1 ՀԴՄ օպերատորների և բաժինների ստացում
        /// </summary>
        /// <returns></returns>
        public UsersAndDepartments GetUsersDepsList()
        {
            Manager.Connect();
            UsersAndDepartments operatorsAndDeps = Manager.IsConnected ? Manager.GetOperatorsAndDeps() : null;
            Result = Manager.Response;
            Manager.Dispose();

            return operatorsAndDeps;
        }

        /// <summary>
        /// 4.6 Սարքի ամսաթվի և ժամի ստացում
        /// </summary>
        /// <returns></returns>
        public string GetEcrDateTime()
        {
            string dateTime = null;
            if (Manager.OperatorLogin())
            {
                Manager.Connect();
                dateTime = Manager.GetEcrDateTime();
            }
            Manager.Dispose();
            Result = Manager.Response;
            return dateTime;
        }

        /// <summary>
        /// 4.8 ՀԴՄ սարքում առկա վճարային համակարգերի ցանկի ստացում
        /// (պահանջվում է ընթացիկ սեսիա)։
        /// </summary>
        /// <returns></returns>
        public PaymentSystemsModel GetPaymentSystems()
        {
            PaymentSystemsModel paymentSystems = null;
            try
            {
                if (Manager.OperatorLogin())
                {
                    paymentSystems = Manager.IsConnected ? Manager.GetPaymentSystemsList() : null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Data adapter (Payment systems): {0}", ex.Message));
            }
            finally
            {
                Result = Manager.Response;
                Manager.Dispose();
            }
            return paymentSystems;
        }
    }
}
