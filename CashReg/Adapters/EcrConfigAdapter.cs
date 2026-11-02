using CashReg.Interfaces;
using System;

namespace CashReg.Adapters
{

    public class EcrConfigAdapter : EcrAdapterBase
    {
        public EcrConfigAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback)
        {

        }

        #region Ecr configuration
        /// <summary>
        /// 4.6.3 ՀԴՄ էջագլխի և էջատակի կարգավորում
        /// </summary>
        public bool ConfigureReceiptHeaderFooter(string header)
        {
            bool pr = false;
            if (!Manager.OperatorLogin())
            {
                pr = Manager.ConfigureReceiptHeader();
                Result = Manager.Response;
                Manager.Dispose();
            }
            else
            {
                Result = Manager.Response;
                Manager.Dispose();
                return false;
            }

            return pr;
        }

        /// <summary>
        /// 4.6.4 ՀԴՄ էջագլխի կարգավորում (Լոգո)
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public bool ConfigureReceiptLogo(string header)
        {
            return false;
        }

        /// <summary>
        /// 4.7 ՀԴՄ համաժամանակեցում
        /// </summary>
        public bool EcrSyncronization()
        {
            bool pr = false;
            if (!Manager.OperatorLogin())
            {
                pr = Manager.SyncEcr();
                Result = Manager.Response;
                Manager.Dispose();
            }
            else
            {
                Result = Manager.Response;
                Manager.Dispose();
                return false;
            }

            return pr;
        }        
        #endregion
    }
}
