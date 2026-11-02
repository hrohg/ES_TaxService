using System;
using CashReg.Interfaces;
using CashReg.Models;

namespace CashReg.Managers
{
    public class CashManager : RequestsManager
    {

        #region Constructors
        public CashManager(IEcrSettings settings, Action<string> errorCallBack, Action<string> exceptionCallBack) : base(settings, errorCallBack, exceptionCallBack)
        {
            OperatorLogin();
        }
        #endregion

        #region External methods
        /// <summary>
        /// 4.5.5 Կանխիկի մուտք, կանխիկի ելք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="isCashIn"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool SetCash(double amount, bool isCashIn, string description)
        {
            if (!IsConnected) { return false; }
            var requestObject = new CashModel(++Seq, amount, isCashIn, _ecrCashier.Cashier, description);
            var request = new EcrRequestModel(OP_PRINT_REPORT, requestObject, SessionKey);
            var requestStream = request.GetRequest();
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, PasswordKey);
                Response = new Helper.EcrResponse(response.ResponseCode);
                if (!response.HasResponse) return true;
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                return true;
            }
            catch (Exception ex)
            {
                SendException(string.Format("Set Cash exception: IsCashIn:{0} error:{1}", isCashIn, ex.Message));
                return false;
            }
        }
        #endregion

    }
}
