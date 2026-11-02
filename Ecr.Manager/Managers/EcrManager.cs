using System;
using System.Collections.Generic;
using System.Linq;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;
using CashReg.Models;
using TaxServiceInvoices.Helper;
using TaxServiceInvoices.Managers;
using CashReg.Adapters;
using Ecr.Manager.Interfaces;
using System.Drawing;
using Ecr.Manager.ViewModels;
using Ecr.Manager.Views;
using System.Windows.Forms;
using Ecr.Manager.WinForm.Controls;
using CashReg;
using static CashReg.Helper.EcrResponse;


//[assembly: CLSCompliant(true)]
namespace Ecr.Manager.Managers
{
    /// <summary>
    /// EcrServer
    /// </summary>
    //[ComVisible(true)]
    public class EcrManager : IEcrManager
    {
        public event OnMessageDelegate OnMessageEvent;
        public event OnErrorDelegate OnErrorEvent;

        protected Action<string> asyncCallback;
        protected IEcrSettings EcrSettings;
        protected EcrAdapterBase EcrAdapter;
        public EcrResponse Result { get; private set; }

        public EcrManager(IEcrSettings ecrSettings)
        {
            EcrSettings = ecrSettings;
        }

        #region Internal Methods

        private Bitmap GetLogo()
        {
            try
            {
                string path = "../ecr-integration/src/main/resources/logo.bmp";
                return new Bitmap(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool SetLogo()
        {
            Bitmap image = GetLogo();
            byte[] bitmapBytes = image != null ? EcrConvert.ImageToByteArray(image) : null;
            return false;
        }

        private int GetTicketValue()
        {
            var form = new InputForm("Վերադարձի կտրոն");
            if (form.ShowDialog() == DialogResult.OK) return HgConvert.ToInt32(form.TicketValue);
            Result = new EcrResponse(ResponseCodes.CanceledByUser);
            return -1;
        }

        [STAThread]
        private int GetTicketData(string crn)
        {
            var vm = new ReturnTicketViewModel(EcrSettings, crn);
            var returnTicket = new ReturnTicketWindow(vm);
            if (true == returnTicket.ShowDialog())
            {
                Result = new EcrResponse(ResponseCodes.CanceledByUser);
                if (asyncCallback != null) { asyncCallback(Result.Description); }
                return (int)Result.Code;
            }
            return -1;
        }
        private List<TotalModel> SelectAndPrintReceiptReturnTicket(List<TotalModel> returnItemsList)
        {
            if (returnItemsList == null) return null;
            var selectproductForm = new SelectProductForm(returnItemsList.OrderBy(s => s.rpid).ToList());
            if (selectproductForm.ShowDialog() != DialogResult.OK)
            {
                Result = new EcrResponse(ResponseCodes.CanceledByUser);
                return null;
            }
            return selectproductForm.Products;
        }

        protected void OnError(string message, string title)
        {
            MessageManager.ShowMessage(message, title);
            OnError(new Exception(message));
        }
        protected void OnMessage(string message)
        {
            var handler = OnMessageEvent;
            if (handler != null) handler(message);
        }
        protected void OnError(Exception ex)
        {
            var handler = OnErrorEvent;
            if (handler != null) handler(ex);
        }
        #endregion

        #region External Methods 

        #region Connections
        /// <summary>
        /// Կապի ստուգում
        /// </summary>
        /// <returns></returns>
        public bool TryConnection()
        {
            var result = new EcrConnectionAdapter(EcrSettings, OnMessage, OnError).TryConnection();

            return result;
        }

        /// <summary>
        /// ՀԴՄ կապի ստուգում
        /// </summary>
        /// <returns></returns>
        public bool TryEcrConnection()
        {
            EcrAdapter = new EcrConnectionAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrConnectionAdapter)EcrAdapter).TryEcrConnection();
            Result = EcrAdapter.Result;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool TryOperatorLogin()
        {
            EcrAdapter = new EcrConnectionAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrConnectionAdapter)EcrAdapter).TryOperatorLogin();
            Result = EcrAdapter.Result;
            return result;
        }
        #endregion Connections        

        #region Print        
        /// <summary>
        /// 4.5.4 ՀԴՄ կտրոնի տպում
        /// Advanced Register method
        /// </summary>
        /// <param name="productItems"></param>
        /// <param name="paid"></param>
        /// <param name="tickedAdditonalData"></param>
        /// <returns></returns>
        public ResponseReceiptModel PrintReceiptTicket(List<IEcrProduct> productItems, IEcrPaid paid, IEcrTickedAdditonalData tickedAdditonalData)
        {
            if (!string.IsNullOrEmpty(tickedAdditonalData.PartnerTin) && paid.PaidAmount > 0)
            {
                MessageManager.ShowMessage("Իրավաբանական անձանց կանխիկ վաճառքն արգելված է: ՀԴՄ կտրոնի տպումը դադարեցվել է:", "ՀԴՄ կտրոնի տպում");
                return null;
            }

            if (paid.PaidAmountCard.HasValue && paid.PaidAmountCard > 0 && !paid.UseExtPos && paid.PaymentSystem == null)
            {
                List<PaymentSystem> paymentSystems = GetPaymentSystems();

                if (paymentSystems.Count > 1)
                {
                    PaymentSystemModel paymentSystem = null;

                    var paymentSystemModels = SelectItemsManager.SelectItem(paymentSystems);
                    paymentSystem = paymentSystemModels.FirstOrDefault();

                    if (paymentSystemModels == null || !paymentSystemModels.Any())
                    {
                        OnError("Անկանխիկ վճարային համակարգ ընտրված չէ: ՀԴՄ կտրոնի տպումը դադարեցվել է:", "ՀԴՄ կտրոնի տպում");
                        return null;
                    }
                    paid.PaymentSystem = paymentSystem != null ? paymentSystem.Code : (int?)null;
                }
            }

            EcrAdapter = new EcrPrintTicketAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrPrintTicketAdapter)EcrAdapter).PrintReceiptTicket(productItems, paid, tickedAdditonalData);
            Result = EcrAdapter.Result;
            return result;
        }

        /// <summary>
        /// 4.5.5 ՀԴՄ վերջին կտրոնի կրկնօրինակի տպում
        /// Այս հրահանգը տպում է վերջին կտրոնի կրկնօրինակը ներառյալ էջագլուխն ու էջատակը:
        /// </summary>
        /// <param name="getResponse"></param>
        /// <returns></returns>
        public bool PrintReceiptLatestCopy(bool getResponse = true)
        {
            EcrAdapter = new EcrPrintTicketAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrPrintTicketAdapter)EcrAdapter).PrintReceiptLatestCopy(getResponse);
            Result = EcrAdapter.Result;
            return result;
        }
        #endregion

        #region Return ticket
        /// <summary>
        /// 4.5.6 ՀԴՄ վերադարձվող կտրոնի ստացում
        /// </summary>
        /// <param name="returnTicketValue"></param>
        /// <returns></returns>
        public ReturnReceiptResponseModel GetReceiptReturnTicket(string returnTicketValue, string crn = null)
        {
            EcrAdapter = new EcrReturnTicketAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrReturnTicketAdapter)EcrAdapter).GetReceiptReturnTicket(returnTicketValue, crn ?? EcrSettings.Crn);
            Result = EcrAdapter.Result;
            MessageManager.ShowMessage(Result.Description, "ՀԴՄ վերադարձվող կտրոնի ստացում");
            return result;
        }

        /// <summary>
        ///  4.5.4 ՀԴՄ կտրոնի վերադարձ
        /// </summary>
        public void PrintReceiptReturnTicket(Action<string> callback = null)
        {
            asyncCallback = callback;
            GetTicketData(EcrSettings.Crn);


            //    //var ticketNumber = GetTicketValue();
            //    //if (ticketNumber == -1) return false;
            //    if (returnAll != null && returnAll.Value)
            //    {
            //        return PrintReceiptReturnTicket(ticketNumber, null, null) != null;
            //    }
            //    else
            //    {
            //        var returnTicket = GetReceiptReturnTicket(ticketNumber.ToString(), "");
            //        if (returnTicket == null)
            //        {
            //            return false;
            //        }
            //        OnError(string.Format("Receiving {0} items", returnTicket.totals.Length));
            //        var products = SelectAndPrintReceiptReturnTicket(returnTicket.totals.ToList());
            //        if (products == null) return false;
            //        ReturnItem[] items = products.Select(i => new ReturnItem(EcrConvert.ConvertToInt(i.rpid), EcrConvert.ConvertToDouble(i.qty))).ToArray();
            //        //var cashAmountForReturn = selectproductForm.Products.Sum(i => i.P * (1 - (i.DscT == "1" ? HgConvert.ToDecimal(i.Dsc) / 100 : 0)));
            //        var cashAmountForReturn = products.Sum(i => EcrConvert.ConvertToDouble(i.qty) * EcrConvert.ConvertToDouble(i.p) * (1 - (i.dsct == "1" ? HgConvert.ToDouble(i.dsc) / 100 : 0)));
            //        return PrintReceiptReturnTicket(ticketNumber, items, new EcrPaid { PartialAmount = cashAmountForReturn, PaidAmountCard = 0 }) != null;
            //    }
        }

        //public ResponseReceiptModel PrintReceiptReturnTicket(IEcrReturnItem[] returnItemsList, EcrPaid paid)
        //{
        //    ResponseReceiptModel responseReceipt = null;
        //    var ticketNumber = GetTicketValue();
        //    if (ticketNumber == -1) return null;
        //    //var server = new ReturnReceiptManager(EcrSettings);
        //    //server.OnException += OnException;
        //    //server.OnErrorReceived += OnError;
        //    //var responseReceipt = server.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
        //    //ActionCode = server.ResponseCode;

        //    if (Manager.OperatorLogin())
        //    {
        //        responseReceipt = Manager.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
        //        ActionCode = Manager.ResponseCode;
        //        Manager.Dispose();
        //    }
        //    return responseReceipt;
        //}

        /// <summary>
        /// 4.5.4 ՀԴՄ վերադարձի կտրոնի տպում
        /// </summary>
        /// <param name="ticketNumber"></param>
        /// <param name="returnItemsList"></param>
        /// <param name="paid"></param>
        /// <returns></returns>

        // change  IEcrReturnItem[] returnItemsList
        public ResponseReceiptModel PrintReceiptReturnTicket(string ticketNumber, IEcrReturnItem[] returnItemsList, EcrPaid paid)
        {

            EcrReturnTicketAdapter ecrAdapter = null;
            ReturnReceiptResponseModel ticketData;
            try
            {
                ecrAdapter = new EcrReturnTicketAdapter(EcrSettings, OnMessage, OnError);
                ticketData = ecrAdapter.GetReceiptReturnTicket(ticketNumber.ToString(), EcrSettings.Crn);
                //todo:continue
                //bool isReturnPartly = ticketData.totals.Any(s => returnItemsList.Where(t => t.RpId s.gc);
            }
            catch (Exception ex)
            {
                MessageManager.ShowMessage(ex.Message, "ՀԴՄ կտրոնի վերադարձ");
            }
            finally
            {
                if (null != ecrAdapter)
                {
                    ecrAdapter.Dispose();
                    Result = ecrAdapter.Result;
                }
            }

            return null;
        }

        // Check is this used?
        public ResponseReceiptModel PrintReceiptReturnTicket(int ticketNumber, IEcrReturnItem[] returnItemsList, EcrPaid paid)
        {
            EcrReturnTicketAdapter ecrAdapter = new EcrReturnTicketAdapter(EcrSettings, OnMessage, OnError);
            ResponseReceiptModel result = ecrAdapter.PrintReceiptReturnTicket(ticketNumber, returnItemsList, paid);
            Result = ecrAdapter.Result;

            //    ResponseReceiptModel responseReceipt = null;
            //    //var server = new ReturnReceiptManager(EcrSettings);
            //    //server.OnException += OnException;
            //    //server.OnErrorReceived += OnError;
            //    //var responseReceipt = server.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
            //    //ActionCode = server.ResponseCode;
            //    if (Manager.OperatorLogin())
            //    {
            //        responseReceipt = Manager.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
            //        ActionCode = Manager.ResponseCode;
            //        Manager.Dispose();
            //    }
            //    return responseReceipt;

            return result;
        }

        /// <summary>
        /// 4.5.4 ՀԴՄ վերադարձի կտրոնի ստացում/տպում
        /// </summary>
        /// <param name="crn"></param>
        /// <param name="returnAll"></param>
        /// <returns></returns>        
        //public bool PrintReceiptReturnTicket(string crn, bool? returnAll = null)
        //{
        //    GetTicketData(crn);
        //    var ticketNumber = GetTicketValue();
        //    if (ticketNumber == -1) return false;
        //    if (returnAll != null && returnAll.Value)
        //    {
        //        return PrintReceiptReturnTicket(ticketNumber, null, null) != null;
        //    }
        //    else
        //    {
        //        var returnTicket = GetReceiptReturnTicket(ticketNumber.ToString(), "");
        //        if (returnTicket == null)
        //        {
        //            return false;
        //        }
        //        OnError(string.Format("Receiving {0} items", returnTicket.totals.Length));
        //        var products = SelectAndPrintReceiptReturnTicket(returnTicket.totals.ToList());
        //        if (products == null) return false;
        //        ReturnItem[] items = products.Select(i => new ReturnItem(EcrConvert.ConvertToInt(i.rpid), EcrConvert.ConvertToDouble(i.qty))).ToArray();
        //        //var cashAmountForReturn = selectproductForm.Products.Sum(i => i.P * (1 - (i.DscT == "1" ? HgConvert.ToDecimal(i.Dsc) / 100 : 0)));
        //        var cashAmountForReturn = products.Sum(i => EcrConvert.ConvertToDouble(i.qty) * EcrConvert.ConvertToDouble(i.p) * (1 - (i.dsct == "1" ? HgConvert.ToDouble(i.dsc) / 100 : 0)));
        //        return PrintReceiptReturnTicket(ticketNumber, items, new EcrPaid { PartialAmount = cashAmountForReturn, PaidAmountCard = 0 }) != null;
        //    }
        //}       

        ///// <summary>
        ///// 4.5.4 ՀԴՄ վերադարձի կտրոնի տպում
        ///// </summary>
        ///// <param name="returnItemsList"></param>
        ///// <param name="paid"></param>
        ///// <returns></returns>
        //public ResponseReceiptModel PrintReceiptReturnTicket(List<TotalModel> returnItemsList, EcrPaid paid)
        //{
        //    var products = SelectAndPrintReceiptReturnTicket(returnItemsList);
        //    if (products == null) return null;
        //    ReturnItem[] items = returnItemsList.Select(i => new ReturnItem(EcrConvert.ConvertToInt(i.rpid), EcrConvert.ConvertToDouble(i.qty))).ToArray();
        //    var cashAmountForReturn = returnItemsList.Sum(i => EcrConvert.ConvertToDouble(i.qty) * EcrConvert.ConvertToDouble(i.p) * (1 - (i.dsct == "1" ? HgConvert.ToDouble(i.dsc) / 100 : 0)));
        //    return new EcrReturnTicketAdapter(EcrSettings).PrintReceiptReturnTicket(items, paid ?? new EcrPaid { PaidAmount = cashAmountForReturn, PaidAmountCard = 0 });

        //}

        //todo: unused, untested

        /// <summary>
        /// ՀԴՄ կտրոնի տպում Excel ֆայլից
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="addVat"></param>
        /// <param name="partnerTin"></param>
        /// <returns></returns>
        public bool PrintReceiptFromExcelFile(string filePath = null, bool addVat = true, IEcrTickedAdditonalData tickedAdditonalData = null)
        {
            //Get Product data
            var t = ExcelImportManager.ImportSaleInvoice(filePath, addVat);
            //Tuple<InvoiceModel, List<InvoiceItemsModel>> t = ExcelImportManager.ImportSaleInvoice(addVAT);
            if (t == null || t.Item1 == null || t.Item2 == null)
            {
                MessageManager.ShowMessage(
                    "Գործողությունն ընդհատված է։ \nՏվյալները կարդալու ժամանակ տեղի է ունեցել սխալ։ Տվյալները թերի են կամ ոչ Լիարժեք։",
                    "Գործողության ընդհատում");
                return false;
            }
            var invoice = t.Item1;
            var invoiceItems = t.Item2;
            if (invoiceItems.Any(s => string.IsNullOrEmpty(s.HcdCs)))
            {
                MessageManager.ShowMessage(string.Format("Գործողությունն ընդհատված է։ \nԱԴԳ կոդը բացակայում է ({0})։", invoiceItems.First(s => string.IsNullOrEmpty(s.HcdCs)).Description),
                    "Գործողության ընդհատում");
                return false;
            }
            if (MessageBox.Show("Տվյալները կարդացվել են հաջողությամբ։ \nԴուք ցանկանու՞մ եք տպել " + (invoice.Summ != null ?
                ((Math.Round(((decimal)invoice.Summ + 5) / 10) * 10) >= invoice.Summ ? (Math.Round(((decimal)invoice.Summ + 5) / 10) * 10) : (decimal)invoice.Summ).ToString("#,###") : "0") +
                                " դրամի հսկիչ դրամարկղային կտրոն", "Հարցում", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                while (true)
                {
                    var response = PrintReceiptTicket(invoiceItems.Select(s =>
                                (IEcrProduct)new EcrProduct(s.Code, s.Description, s.Mu)
                                {
                                    qty = s.Quantity ?? 0,
                                    price = s.Price ?? 0,
                                    adgCode = s.HcdCs,
                                }).ToList(),
                                new EcrPaid { PaidAmount = invoice.Summ != null ? (int)(Math.Round(((decimal)invoice.Summ + 5) / 10) * 10) : 0 }, tickedAdditonalData);
                    if (Result.Code == ResponseCodes.Ok)
                    {
                        MessageManager.ShowMessage(
                            "Հսկիչ դրամարկղային կտրոնի տպումն իրականացվել է հաջողությամբ։ \nԽնդրում ենք վերցնել հսկիչ դրամարկղային կտրոնը։",
                            "Տեղեկացում");
                        return true;
                    }
                    else
                    {
                        if (MessageBox.Show(
                            "Սխալ։" + " (error code " + Result + ")" +
                            " \nՀսկիչ դրամարկղային կտրոնի տպումն ընդհատվել է։ \nՏպե՞լ ևս մեկ անգամ։",
                            "Տեղեկացում",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }
            }
            return false;
        }
        #endregion Return ticket

        /// <summary>
        /// 4.5.1  ՀԴՄ օպերատորների և բաժինների ստացում
        /// </summary>
        /// <returns></returns>
        public UsersAndDepartments GetUsersDepsList()
        {
            EcrAdapter = new EcrDataAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrDataAdapter)EcrAdapter).GetUsersDepsList();
            Result = EcrAdapter.Result;
            return result;
        }

        #region 4.5.8 Cash receipts or withdrawal

        /// <summary>
        /// 4.5.8 Կանխիկի մուտք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool SetCashReceipt(double amount, string description)
        {
            EcrAdapter = new EcrCashWithdrawalAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrCashWithdrawalAdapter)EcrAdapter).SetCash(IsCashInEnum.CashInReceipt, amount, description);
            Result = EcrAdapter.Result;
            return result;
        }

        /// <summary>
        /// 4.5.8 Կանխիկի ելք
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool SetCashWithdrawal(double amount, string description)
        {
            EcrAdapter = new EcrCashWithdrawalAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrCashWithdrawalAdapter)EcrAdapter).SetCash(IsCashInEnum.CashWithdrawal, amount, description);
            Result = EcrAdapter.Result;
            return result;
        }
        #endregion

        /// <summary>
        /// 4.6 Սարքի ամսաթվի և ժամի ստացում
        /// </summary>
        /// <returns></returns>
        public string GetEcrDateTime()
        {
            EcrAdapter = new EcrDataAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrDataAdapter)EcrAdapter).GetEcrDateTime();
            Result = EcrAdapter.Result;
            return result;
        }

        #region 4.6.1 Blank ticket

        /// <summary>
        /// 4.6.1 Կտրոնի ձևանմուշի տպում
        /// </summary>
        public void PrintBlankTicket()
        {
            EcrAdapter = new EcrPrintTicketAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrPrintTicketAdapter)EcrAdapter).PrintBlankTicket();
            Result = EcrAdapter.Result;
        }

        #endregion Print blank

        #region 4.6.2 Report

        /// <summary>
        /// 4.6.2 ՀԴՄ հաշվետվության տպում
        /// Կոդավորումը՝ սեսիայի բանալիով
        /// </summary>
        /// <param name="report"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetReport(ReportType report, long startDate, long endDate)
        {
            EcrAdapter = new EcrReportAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrReportAdapter)EcrAdapter).GetReport(report, startDate, endDate);
            Result = EcrAdapter.Result;
            return result;
        }

        /// <summary>
        /// ՀԴՄ հաշվետվության տպում
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public bool GetReport(ReportType report)
        {
            var dateIntermediateForm = new SelectDateIntermediateForm();
            if (dateIntermediateForm.ShowDialog() != DialogResult.OK)
            {
                MessageManager.ShowMessage("Գործողությունն ընդհատվել է օգտագործողի կողմից։", "Գործողության ընդհատում");
                return false;
            }
            return GetReport(report, EcrConvert.ConvertDatetimeToUnixTimestamp(dateIntermediateForm.StartDate), EcrConvert.ConvertDatetimeToUnixTimestamp(dateIntermediateForm.EndDate));
        }

        /// <summary>
        /// ՀԴՄ X հաշվետվության տպում
        /// </summary>
        /// <returns></returns>
        public bool GetReportX()
        {
            return GetReport(ReportType.X);
        }

        /// <summary>
        /// ՀԴՄ X հաշվետվության տպում
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetReportX(int startDate, int endDate)
        {
            return GetReport(ReportType.X, startDate, endDate);
        }

        /// <summary>
        /// ՀԴՄ Z հաշվետվության տպում
        /// </summary>
        /// <returns></returns>
        public bool GetReportZ()
        {
            return GetReport(ReportType.Z);
        }

        /// <summary>
        /// ՀԴՄ Z հաշվետվության տպում
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool GetReportZ(int startDate, int endDate)
        {
            return GetReport(ReportType.Z, startDate, endDate);
        }
        #endregion

        /// <summary>
        /// 4.7 ՀԴՄ համաժամանակեցում
        /// </summary>
        public bool EcrSyncronization()
        {
            EcrAdapter = new EcrConfigAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrConfigAdapter)EcrAdapter).EcrSyncronization();
            Result = EcrAdapter.Result;
            return result;
        }

        /// <summary>
        /// 4.8 ՀԴՄ սարքում առկա վճարային համակարգերի ցանկի ստացում
        /// (պահանջվում է ընթացիկ սեսիա)։
        /// </summary>
        /// <returns></returns>
        public List<PaymentSystem> GetPaymentSystems()
        {
            EcrDataAdapter ecrAdapter = new EcrDataAdapter(EcrSettings, OnMessage, OnError);
            PaymentSystemsModel result = ecrAdapter.GetPaymentSystems();
            Result = ecrAdapter.Result;
            ecrAdapter.Dispose();

            return result.PaymentSystems.ToList();
        }

        #region Ecr Configure
        public bool ConfigureReceiptHeaderFooter()
        {
            var header = string.Empty;
            EcrAdapter = new EcrConfigAdapter(EcrSettings, OnMessage, OnError);
            var result = ((EcrConfigAdapter)EcrAdapter).ConfigureReceiptHeaderFooter(header);
            Result = EcrAdapter.Result;
            return result;
        }
        #endregion

        #endregion
    }
}
