using System;
using System.Windows.Forms;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Models;

namespace CashReg.Managers
{
    internal class ReturnReceiptManager : RequestsManager
    {
        #region Contructors
        public ReturnReceiptManager(IEcrSettings ecrSettings, Action<string> errorCallBack, Action<string> exceptionCallBack)
            : base(ecrSettings, errorCallBack, exceptionCallBack)
        {
            OperatorLogin();
        }
        #endregion Constructors

        #region External methods
        /// <summary>
        /// 4.5.3 ՀԴՄ վերադարձվող կտրոնի ստացում
        /// </summary>
        /// <param name="returnTicketValue">Վերադարձվող կտրոնի համար</param>
        /// <param name="crn">Վերադարձվող կտրոն տպած սարքի գրանցման համար</param>
        /// <returns>ReturnReceiptResponseModel</returns>
        public ReturnReceiptResponseModel GetReturnReceipt(string returnTicketValue, string crn)
        {
            if (SessionKey == null)
            {
                return null;
            }
            var requestObject = new ReturnReceiptRequestModel(++Seq, returnTicketValue, Crn);
            var request = new EcrRequestModel(OP_GET_RECEIPT_DATA, requestObject, SessionKey);

            var requestStream = request.GetRequest();
            try
            {
                var receave = new byte[11];
                Socket.Send(requestStream, requestStream.Length, 0);
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                if (!response.HasResponse)
                {
                    SendError(response.ResponseCode, "", request.GetRequestByJson, response.ToString());
                    Response = new EcrResponse(EcrResponse.ResponseCodes.EcrResponceException);
                    return null;
                }

                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;

                var value = response.GetResponse<ReturnReceiptResponseModel>();
                return value;
            }
            catch (TimeoutException ex)
            {
                SendError(Response.Code, ex.Message);
                //MessageBox.Show("Timeout UnhandledExceptionEventArgs", "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Response = new EcrResponse(EcrResponse.ResponseCodes.NetworkConnectionError);
                return null;
            }
            catch (Exception ex)
            {
                SendError(Response.Code, ex.Message);
                //MessageBox.Show(ex.Message, "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Response = new EcrResponse(EcrResponse.ResponseCodes.EcrResponceException);
                return null;
            }
        }

        /// <summary>
        /// 4.5.4 ՀԴՄ վերադարձի կտրոնի տպում
        /// </summary>
        /// <param name="crn">Սարքի գրանցման համար</param>
        /// <param name="returnTicketValue">Վերադարձվող կտրոնի համար</param>
        /// <param name="returnItemsList">Վերադարձվող ապրանքների տվյալներ</param>
        /// <param name="paid">վերադարձվող գումար:</param>
        /// <returns></returns>
        public ResponseReceiptModel PrintReturnReceipt(string crn, int returnTicketValue, IEcrReturnItem[] returnItemsList, EcrPaid paid)
        {
            if (!IsConnected)
            {
                return null;
            }
            var requestObject = new PrintReturnReceiptRequestModel(++Seq, crn, returnTicketValue, returnItemsList, paid != null ? paid.PaidAmount : 0, paid != null ? paid.PaidAmountCard : 0);
            var request = new EcrRequestModel(OP_PRINT_RETURN_RECEIPT, requestObject, SessionKey);
            var requestStream = request.GetRequest();
            try
            {
                Socket.Send(requestStream, requestStream.Length, 0);
                var receave = new byte[11];
                Socket.Receive(receave);
                var response = new EcrResponseModel(receave, SessionKey);
                Response = new EcrResponse(response.ResponseCode);
                if (!response.HasResponse) return null;
                receave = new byte[response.ResponseLength];
                Socket.Receive(receave);
                response.Response = receave;
                var value = response.GetResponse<ResponseReceiptModel>();
                return value;
            }
            catch (Exception ex)
            {
                MessageManager.ShowMessage(ex.Message, "Error message");
                return null;
            }
        }
        #endregion


    }
}
