using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CashReg.Classes;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;
using CashReg.Models;
using TaxServiceInvoices.Helper;
using TaxServiceInvoices.Managers;

//[assembly: CLSCompliant(true)]
namespace CashReg
{
    /// <summary>
    /// 
    /// </summary>
    //[ComVisible(true)]
    public class EcrServer : EcrServerBase
    {
        #region Internal properties

        /// <summary>
        /// Author detail
        /// </summary>
        private const string _version = "FV15.1214.337.05";
        private const string _aboutVersion = "This is free version of the Electric Cash Register. All rights reserved.";
        private const string _author = "Hrayr Gishyan";
        private const string _authorContacts = "e-mail:HrayrGishyan@gmail.com \n Moble:+374 94 88 19 83";

        /// <summary>
        /// Error code detail
        /// </summary>

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
        public EcrException.ResponseCodes ActionCode
        {
            get { return ErrorCode; }
            private set
            {
                ErrorCode = value;
            }
        }
        public string ActionDescription
        {
            get { return EcrException.GetExceptionDescripyion(ActionCode); }
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
        /// <param name="ecrSettings"></param>
        public EcrServer(IEcrSettings ecrSettings)
        {
            EcrSettings = ecrSettings;
            Initialize();
        }
        #endregion

        #region Internal Methods
        protected override void OnError(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = string.Format("{0} ({1}) ", ActionDescription, ActionCode);
            }
            try
            {
                var handler = OnErrorEvent;
                if (handler != null) handler(new Exception(message));
                
                //if (Manager != null) Manager.Dispose();
            }
            catch (Exception ex)
            {
                var handler = OnErrorEvent;
                if (handler != null) handler(ex);
            }
        }

        protected override void OnException(string message)
        {
            EcrException.OnException(message);
            Manager.Dispose();
        }

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
        private bool SetCash(IsCashInEnum isCashIn, decimal amount, string description)
        {
            var manager = new CashManager(EcrSettings);
            manager.SetCash(amount, isCashIn == IsCashInEnum.CashInReceipt, description);
            ActionCode = manager.ResponseCode;
            manager.Dispose();
            return (ActionCode == EcrException.ResponseCodes.Ok);
        }
        private int GetTicketValue()
        {
            var form = new InputForm("Վերադարձի կտրոն");
            if (form.ShowDialog() == DialogResult.OK) return HgConvert.ToInt32(form.TicketValue);
            ActionCode = EcrException.ResponseCodes.CanceledByUser;
            return -1;
        }
        private List<TotalModel> SelectAndPrintReceiptReturnTicket(List<TotalModel> returnItemsList)
        {
            if (returnItemsList == null) return null;
            var selectproductForm = new SelectProductForm(returnItemsList.OrderBy(s => s.RpId).ToList());
            if (selectproductForm.ShowDialog() != DialogResult.OK)
            {
                ActionCode = EcrException.ResponseCodes.CanceledByUser;
                return null;
            }
            return selectproductForm.Products;
        }
        #endregion

        #region External Methods

        #region Connections
        public override bool TryConnection()
        {
            var isConnected = Manager.Connect();
            Manager.Disconnect();
            return isConnected;
        }
        public override bool TryEcrConnection()
        {
            Manager.TryCheckEcrConnection();
            ActionCode = Manager.ResponseCode;
            Manager.Dispose();
            return ActionCode == EcrException.ResponseCodes.Ok;
        }
        public override bool TryOperatorLogin()
        {
            var isOperatorLogin = Manager.TryOperatorLogin();
            ActionCode = Manager.ResponseCode;
            Manager.Dispose();
            return isOperatorLogin;
        }
        public override UsersAndDepartments GetUsersDepsList()
        {
            UsersAndDepartments operatorsAndDeps = null;
            if (Manager.OperatorLogin())
            {
                operatorsAndDeps = Manager.IsConnected ? Manager.GetOperatorsAndDeps() : null;
            }
            Manager.Dispose();
            ActionCode = Manager.ResponseCode;
            return operatorsAndDeps;

        }
        public override bool OperatorLogout()
        {
            return Manager.IsOperatorLogin && Manager.OperatorLogout();
        }
        #endregion

        #region Print
        //Advanced Register method
        public override ResponseReceiptModel PrintReceipt(List<IEcrProduct> productItems, IEcrPaid paid, string partnerTin)
        {
            if (Manager.OperatorLogin())
            {
                var pr = Manager.PrintReceipt(productItems, paid, partnerTin);
                ActionCode = Manager.ResponseCode;
                Manager.Dispose();
                return pr;
            }
            else
            {
                ActionCode = Manager.ResponseCode;
                OnError(null);
                Manager.Disconnect();
                return null;
            }
        }
        public override bool PrintReceiptLatestCopy(bool getResponse = true)
        {
            if (EcrSettings == null) { return false; }
            if (!Manager.IsConnected || !Manager.OperatorLogin())
            {
                ActionCode = Manager.ResponseCode;
                Manager.Disconnect();
                return false;
            }
            var result = Manager.PrintReceiptLatestCopy();
            ActionCode = Manager.ResponseCode;
            Manager.Dispose();
            return result;
        }
        public override ReturnReceiptResponseModel GetReceiptReturnTicket(string returnTicketValue)
        {
            ReturnReceiptResponseModel returnReceipt = null;
            //var server = new ReturnReceiptManager(EcrSettings);
            //server.OnException += OnException;
            //server.OnErrorReceived += OnError;
            //var returnReceipt = server.GetReturnReceipt(returnTicketValue);
            //ActionCode = server.ResponseCode;
            //server.Dispose();

            if (Manager.OperatorLogin())
            {
                returnReceipt = Manager.GetReturnReceipt(returnTicketValue);
                ActionCode = Manager.ResponseCode;
                Manager.Dispose();
            }
            return returnReceipt;
        }
        public override bool PrintReceiptReturnTicket(bool? returnAll = null)
        {
            var ticketNumber = GetTicketValue();
            if (ticketNumber == -1) return false;
            if (returnAll != null && returnAll.Value)
            {
                return PrintReceiptReturnTicket(ticketNumber, null, null) != null;
            }
            else
            {
                var returnTicket = GetReceiptReturnTicket(ticketNumber.ToString());
                if (returnTicket == null)
                {
                    return false;
                }
                OnError(string.Format("Receiving {0} items", returnTicket.Totals.Length));
                var products = SelectAndPrintReceiptReturnTicket(returnTicket.Totals.ToList());
                if (products == null) return false;
                ReturnItem[] items = products.Select(i => new ReturnItem(EcrConvert.ConvertToInt(i.RpId), EcrConvert.ConvertToDouble(i.Qty))).ToArray();
                //var cashAmountForReturn = selectproductForm.Products.Sum(i => i.P * (1 - (i.DscT == "1" ? HgConvert.ToDecimal(i.Dsc) / 100 : 0)));
                var cashAmountForReturn = products.Sum(i => EcrConvert.ConvertToDouble(i.Qty) * EcrConvert.ConvertToDouble(i.P) * (1 - (i.DscT == "1" ? HgConvert.ToDouble(i.Dsc) / 100 : 0)));
                return PrintReceiptReturnTicket(ticketNumber, items, new EcrPaid { PartialAmount = cashAmountForReturn, PaidAmountCard = 0 }) != null;
            }
        }
        public override ResponseReceiptModel PrintReceiptReturnTicket(IEcrReturnItem[] returnItemsList, EcrPaid paid)
        {
            ResponseReceiptModel responseReceipt = null;
            var ticketNumber = GetTicketValue();
            if (ticketNumber == -1) return null;
            //var server = new ReturnReceiptManager(EcrSettings);
            //server.OnException += OnException;
            //server.OnErrorReceived += OnError;
            //var responseReceipt = server.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
            //ActionCode = server.ResponseCode;

            if (Manager.OperatorLogin())
            {
                responseReceipt = Manager.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
                ActionCode = Manager.ResponseCode;
                Manager.Dispose();
            }
            return responseReceipt;
        }
        public override ResponseReceiptModel PrintReceiptReturnTicket(int ticketNumber, IEcrReturnItem[] returnItemsList, EcrPaid paid)
        {
            ResponseReceiptModel responseReceipt = null;
            //var server = new ReturnReceiptManager(EcrSettings);
            //server.OnException += OnException;
            //server.OnErrorReceived += OnError;
            //var responseReceipt = server.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
            //ActionCode = server.ResponseCode;
            if (Manager.OperatorLogin())
            {
                responseReceipt = Manager.PrintReturnReceipt(ticketNumber, returnItemsList, paid);
                ActionCode = Manager.ResponseCode;
                Manager.Dispose();
            }
            return responseReceipt;
        }

        public override ResponseReceiptModel PrintReceiptReturnTicket(List<TotalModel> returnItemsList, EcrPaid paid)
        {
            var products = SelectAndPrintReceiptReturnTicket(returnItemsList);
            if (products == null) return null;
            ReturnItem[] items = products.Select(i => new ReturnItem(EcrConvert.ConvertToInt(i.RpId), EcrConvert.ConvertToDouble(i.Qty))).ToArray();
            var cashAmountForReturn = products.Sum(i => EcrConvert.ConvertToDouble(i.Qty) * EcrConvert.ConvertToDouble(i.P) * (1 - (i.DscT == "1" ? HgConvert.ToDouble(i.Dsc) / 100 : 0)));
            return PrintReceiptReturnTicket(items, paid ?? new EcrPaid { PaidAmount = cashAmountForReturn, PaidAmountCard = 0 });

        }
        //todo: unused, untested
        public override bool PrintReceiptFromExcelFile(string filePath = null, bool addVat = true, string partnerTin = null)
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
                    var response = PrintReceipt(invoiceItems.Select(s =>
                                (IEcrProduct)new EcrProduct(s.Code, s.Description, s.Mu)
                                {
                                    Qty = s.Quantity ?? 0,
                                    Price = s.Price ?? 0,
                                    AdgCode = s.HcdCs,
                                }).ToList(),
                                new EcrPaid { PaidAmount = invoice.Summ != null ? (int)(Math.Round(((decimal)invoice.Summ + 5) / 10) * 10) : 0 }, partnerTin);
                    if (ActionCode == EcrException.ResponseCodes.Ok)
                    {
                        MessageManager.ShowMessage(
                            "Հսկիչ դրամարկղային կտրոնի տպումն իրականացվել է հաջողությամբ։ \nԽնդրում ենք վերցնել հսկիչ դրամարկղային կտրոնը։",
                            "Տեղեկացում");
                        return true;
                    }
                    else
                    {
                        if (MessageBox.Show(
                            "Սխալ։" + ActionDescription + " (error code " + ActionCode + ")" +
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
        #endregion

        #region Report
        public override bool GetReport(ReportType report, long startDate, long endDate)
        {
            Manager.OperatorLogin();
            var reportManager = new EcrReportManager(report, startDate, endDate, Manager);
            reportManager.GetReport();
            ActionCode = Manager.ResponseCode;
            Manager.Dispose();
            return (ActionCode == EcrException.ResponseCodes.Ok);
        }
        public override bool GetReport(ReportType report)
        {
            var dateIntermediateForm = new SelectDateIntermediateForm();
            if (dateIntermediateForm.ShowDialog() != DialogResult.OK)
            {
                MessageManager.ShowMessage("Գործողությունն ընդհատվել է օգտագործողի կողմից։",
                                "Գործողության ընդհատում");
                return false;
            }
            return GetReport(report, EcrConvert.ConvertDatetimeToUnixTimestamp(dateIntermediateForm.StartDate), EcrConvert.ConvertDatetimeToUnixTimestamp(dateIntermediateForm.EndDate));
        }
        #endregion

        #region Header Footer
        public bool ConfigureReceiptHeader()
        {
            bool pr = false;
            if (!Manager.OperatorLogin())
            {

                pr = Manager.ConfigureReceiptHeader();
                ActionCode = Manager.ResponseCode;
                Manager.Dispose();
            }
            else
            {
                ActionCode = Manager.ResponseCode;
                Manager.Dispose();
                return false;
            }

            return pr;
        }
        #endregion

        #region Cash receipts or withdrawal
        public override bool SetCashReceipt(decimal amount, string description)
        {
            return SetCash(IsCashInEnum.CashInReceipt, amount, description);
        }
        public override bool SetCashWithdrawal(decimal amount, string description)
        {
            return SetCash(IsCashInEnum.CashWithdrawal, amount, description);
        }
        #endregion

        #endregion

        #region Evens
        /// <summary>
        /// On Error event
        /// </summary>
        public override event OnErrorDelegate OnErrorEvent;
        #endregion Events
    }
}
