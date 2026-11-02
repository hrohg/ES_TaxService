using System;
using System.Collections.Generic;
using System.Linq;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Models;
using TaxServiceInvoices.Helper;

namespace CashReg.Adapters
{
    public class EcrReturnTicketAdapter : EcrAdapterBase
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ecrSettings"></param>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ecrSettings"></param>
        public EcrReturnTicketAdapter(IEcrSettings ecrSettings, Action<string> messageCallback, Action<Exception> exceptionCallback) : base(ecrSettings, messageCallback, exceptionCallback)
        {

        }
        #endregion Constructors

        #region External methods

        /// <summary>
        /// ՀԴՄ վերադարձվող կտրոնի ստացում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <returns></returns>

        #region return ticket

        /// <summary>
        /// 4.5.3 ՀԴՄ վերադարձվող կտրոնի ստացում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <param name="crn"></param>
        /// <returns></returns>
        public ReturnReceiptResponseModel GetReceiptReturnTicket(string returnTicketValue, string crn)
        {
            ReturnReceiptResponseModel returnReceipt = null;
            if (Manager.OperatorLogin())
            {
                returnReceipt = Manager.GetReturnReceipt(returnTicketValue, crn);
                Result = Manager.Response;
            }
            else
            {
                Result = Manager.Response;
            }
            return returnReceipt;
        }


        public ResponseReceiptModel PrintReceiptReturnTicket(int ticketNumber, IEcrReturnItem[] returnItemsList = null, EcrPaid paid = null)
        {
            ResponseReceiptModel responseReceipt = null;
            if (Manager.OperatorLogin())
            {
                responseReceipt = Manager.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
                Result = Manager.Response;
                Manager.Dispose();
            }
            return responseReceipt;
        }

        /// <summary>
        /// 4.5.4 ՀԴՄ վերադարձի կտրոնի տպում
        /// </summary>
        /// <param name="returnItemsList"></param>
        /// <param name="paid"></param>
        /// <returns></returns>
        public ResponseReceiptModel PrintReceiptReturnTicket(int ticketNumber, List<TotalModel> returnItemsList, EcrPaid paid)
        {
            if (returnItemsList == null || !returnItemsList.Any()) return null;
            ReturnItem[] items = returnItemsList.Select(i => new ReturnItem(EcrConvert.ConvertToInt(i.rpid), EcrConvert.ConvertToDouble(i.qty))).ToArray();
            var cashAmountForReturn = returnItemsList.Sum(i => EcrConvert.ConvertToDouble(i.qty) * EcrConvert.ConvertToDouble(i.p) * (1 - (i.dsct == "1" ? HgConvert.ToDouble(i.dsc) / 100 : 0)));
            return PrintReceiptReturnTicket(ticketNumber, items, paid ?? new EcrPaid { PaidAmount = cashAmountForReturn, PaidAmountCard = 0 });

        }
        #endregion Return ticket

        #endregion External methods
    }
}
