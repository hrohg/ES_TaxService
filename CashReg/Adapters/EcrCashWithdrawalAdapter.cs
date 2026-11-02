using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;
using System;

namespace CashReg.Adapters
{
    public class EcrCashWithdrawalAdapter : EcrAdapterBase
    {
        public EcrCashWithdrawalAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback) { }


        #region Cash receipts or withdrawal    
        /// <summary>
        /// 4.5.5 Կանխիկի մուտք, կանխիկի ելք
        /// </summary>
        /// <param name="isCashIn"></param>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool SetCash(IsCashInEnum isCashIn, double amount, string description)
        {
            Action<string> action = new Action<string>((message) => { if (ErrorCallback != null) ErrorCallback(new TaxServiceException(message)); });
            var manager = new CashManager(EcrSettings, MessageCallback, action);
            manager.SetCash(amount, isCashIn == IsCashInEnum.CashInReceipt, description);
            Result = manager.Response;
            manager.Dispose();
            return Result.Code == EcrResponse.ResponseCodes.Ok;
        }
        #endregion
    }
}
