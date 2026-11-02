using CashReg.Helper;
using CashReg.Interfaces;
using System;

namespace CashReg.Adapters
{
    public class EcrConnectionAdapter : EcrAdapterBase
    {
        public EcrConnectionAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback) { }


        /// <summary>
        /// Կապի ստուգում
        /// </summary>
        /// <returns></returns>
        public bool TryConnection()
        {
            var isConnected = Manager.Connect();
            Manager.Disconnect();
            return isConnected;
        }

        /// <summary>
        /// ՀԴՄ կապի ստուգում
        /// </summary>
        /// <returns></returns>
        public bool TryEcrConnection()
        {
            Manager.TryCheckEcrConnection();
            Result = Manager.Response;
            Manager.Dispose();
            return Result.Code == EcrResponse.ResponseCodes.Ok;
        }
    }
}
