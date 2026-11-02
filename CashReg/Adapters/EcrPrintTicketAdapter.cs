using CashReg.Adapters;
using CashReg.Interfaces;
using CashReg.Models;
using System;
using System.Collections.Generic;


//[assembly: CLSCompliant(true)]
namespace CashReg
{
    /// <summary>
    /// EcrPrintTicketAdapter
    /// </summary>
    //[ComVisible(true)]
    public class EcrPrintTicketAdapter : EcrAdapterBase
    {
        #region Internal properties        

        #endregion

        #region External properties
        public string Version
        {
            get { return _version; }
        }
        public string About
        {
            get { return _aboutVersion; }
        }
        public string Author
        {
            get { return _author; }
        }
        public string Contacts
        {
            get { return _authorContacts; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ecrSettings"></param>

        /// <summary>
        /// 
        /// </summary>
        public EcrPrintTicketAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback)
        {

        }
        #endregion       

        #region External methods

        /// <summary>
        /// ՀԴՄ կտրոնի տպում
        /// </summary>
        /// <param name="productItems"></param>
        /// <param name="paid"></param>
        /// <param name="partnerTin"></param>
        /// <returns></returns>
        //[ComVisible(true)]
        public ResponseReceiptModel PrintReceiptTicket(List<IEcrProduct> productItems, IEcrPaid paid, IEcrTickedAdditonalData tickedAdditonalData)
        {
            ResponseReceiptModel responseReceiptModel = null;
            try
            {
                if (Manager.OperatorLogin())
                {
                    responseReceiptModel = Manager.PrintReceiptTicket(productItems, paid, tickedAdditonalData);
                    //OnMessage($"VerificationNumber: {responseReceiptModel.VerificationNumber}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ecr print adapter (PrintReceiptTicket): {0}", ex.Message));
            }
            finally
            {
                Result = Manager.Response;
                Manager.Dispose();
            }
            return responseReceiptModel;
        }

        /// <summary>
        /// 4.5.2 ՀԴՄ վերջին կտրոնի կրկնօրինակի տպում
        /// </summary>
        /// <param name="getResponse"></param>
        /// <returns></returns>        
        public bool PrintReceiptLatestCopy(bool getResponse)
        {
            if (EcrSettings == null) { return false; }
            if (!Manager.OperatorLogin())
            {
                Result = Manager.Response;
                Manager.Disconnect();
                return false;
            }

            var result = Manager.PrintReceiptLatestCopy();
            Result = Manager.Response;
            Manager.Dispose();
            return result;
        }

        /// <summary>
        /// 4.6.1 Կտրոնի ձևանմուշի տպում
        /// </summary>
        /// <returns></returns>
        public bool PrintBlankTicket()
        {
            if (EcrSettings == null) { return false; }
            if (!Manager.OperatorLogin())
            {
                Result = Manager.Response;
                Manager.Disconnect();
                return false;
            }

            var result = Manager.PrintBlankTicket();
            Result = Manager.Response;
            Manager.Dispose();
            return result;
        }
        #endregion External methods
    }
}
