using Ecr.Manager.Managers;
using ECR.Services.Helper;
using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CashRegest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            /////AccountingDocumentConverter tests
            //EsMarket.SharedData.Models.EsMarketInvoice invoice = new EsMarket.SharedData.Models.EsMarketInvoice()
            //{
            //    GoodsInfo = new System.Collections.Generic.List<EsMarket.SharedData.Models.EsGoodInfo> {
            //    new EsMarket.SharedData.Models.EsGoodInfo { Code="111", Description="aaaa", HcdCs="1421", Price=100, Quantity=3, Unit="qte" },
            //    new EsMarket.SharedData.Models.EsGoodInfo { Code="112", Description="bbbb", HcdCs="1422", Price=300, Quantity=5, Unit="qte" }
            //}
            //};
            //TaxService.Manager.Helpers.AccountingDocumentConverter.ExportSaleAccDocToXml(invoice);

            //Expression olver Tests
            Tests.ExpressionSolverTest.SimpleExpressions();
            return;

            // Payment systems
            //var allItems = new List<PaymentSystem>
            //                {
            //                    new PaymentSystem { code = 1, name = "Visa" },
            //                    new PaymentSystem { code = 10, name = "TelCell"},
            //                    //new PaymentSystem { code = 11, name = "EasyPay"},
            //                    //new PaymentSystem { code = 12, name = "Mobidram"},
            //                    //new PaymentSystem { code = 13, name = "iDram"},
            //                    //new PaymentSystem { code = 14, name = "StackProcessing"},
            //                    //new PaymentSystem { code = 15, name = "FastShift"},
            //                    //new PaymentSystem { code = 16, name = "TeamPay"},
            //                    //new PaymentSystem { code = 17, name = "Payex"},
            //                    //new PaymentSystem { code = 18, name = "InecoPay"}
            //                };

            //SelectItemsManager.SelectItem(allItems);
            //return;


            //var res = new List<string>();
            //var count = 25;
            //while(count-->0) res.Add(RandomString());
            var config = ConfigSettings.GetEcrConfig();
            //config.Crn = "1234";
            var server = new EcrManager(config.Convert());
            server.PrintReceiptReturnTicket();
            //TaxService.Converter.Helpers.XmlHelper.Save(new ExportedAccDocData(), @"D:\Hrayr\Desktop\010.xml");
            //AccountingDocumentConverter.ExportToXmlAccDoc(new EsMarketInvoice());
            return;

            //var l = EcrConvert.BeByteToInt16(new byte[] { 14, 184 });
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomainAssemblyResolve);
            //new ConvertManager().ExcelAsciiToUnicodeFromCode();
            //TaxService.Converter.Helpers.AccountingDocumentConverter.ConvertAccountingDocumentToXml(@"C:\Users\HrayrG\Desktop\invoice2.xml");
            //TaxService.Converter.Helpers.AccountingDocumentConverter.ConvertAccountingDocumentFromXml(@"C:\Users\HrayrG\Desktop\invoice1.xml");
            //var result = ("123-34-114-115-101-113-34-58-49-48-50-48-44-34-99-114-110-34-58-34-51-49-54-48-55-48-56-56-34-44-34-115-110-34-58-34-81-56-48-52-49-52-53-48-54-54-57-57-34-44-34-116-105-110-34-58-34-55-48-52-49-51-54-51-57-34-44-34-116-97-120-112-97-121-101-114-34-58-34-213-147-213-161-213-183-213-171-213-175-32-212-177-213-189-213-161-213-191-214-128-213-181-213-161-213-182-34-44-34-97-100-100-114-101-115-115-34-58-34-213-134-213-136-213-133-212-181-213-132-212-178-212-181-213-144-213-133-212-177-213-134-32-212-181-214-128-214-135-213-161-213-182-213-181-213-161-213-182-32-49-34-44-34-116-105-109-101-34-58-49-53-52-55-57-56-52-48-57-56-48-48-48-46-48-44-34-102-105-115-99-97-108-34-58-34-57-52-56-51-55-53-57-54-34-44-34-108-111-116-116-101-114-121-34-58-34-48-48-48-48-48-48-48-48-34-44-34-112-114-105-122-101-34-58-48-44-34-116-111-116-97-108-34-58-50-51-57-48-46-48-44-34-99-104-97-110-103-101-34-58-55-54-49-48-46-48-125").Split('-');
            //byte[] recponse = result.Select(s => Byte.Parse(s)).ToArray();
            //var val = EcrJsonConverters.DeserializeObect<ResponseReceiptModel>(recponse); // new JavaScriptSerializer().Deserialize<ResponseReceiptModel>(Encoding.UTF8.GetString(recponse));
            //TaxService.Converter.Helpers.AccountingDocumentConverter.ExportToXmlAccDoc(new EsMarketInvoice { GoodsInfo = new List<EsGoodInfo>() });
            //return;


            //return;
            if (true)
            {
                //config.EcrServiceSettings.Ip = "192.168.0.61";
                var ecrServer = new ECR.Services.Server.SocketServer(config);
                ecrServer.Start();
            }
            else
            {
                if (config == null) config = new EcrConfig();

                //var server = new EcrServer(config.Convert());
                var isConneccted = server.TryConnection();
                //server.PrintReceiptReturnTicket(false);
                //if(!server.TryConnection()) return;
                //if(!server.TryEcrConnection()) return;
                //if(!server.TryOperatorLogin()) return;

                //server.PrintReceiptFromExcelFile(@"C:\Users\HrayrG\Desktop\3080.xlsm", true);
                //server.TryOperatorLogin();
                //server.GetUsersDepsList();

                //server.TryOperatorLogin();

            }

            //server.TryConnection();



            //if (ecrServer.IsStarted)
            //{
            //    var config = ConfigSettings.GetEcrConfig();
            //    var server = new EcrServer(config.EcrSettings);
            //    server.OperatorLogout();
            //}

            //ECR.Services.Program.Main();
            //new ConvertManager().ExcelUnicodeToAsciiByCode();




            //server.PrintReceiptReturnTicket();
            //server.GetReportZ();

            //var requeststring = "";
            //foreach (var b in requestStream)
            //{
            //    requeststring += string.IsNullOrEmpty(requeststring) ? " " : "-") + b;
            //}

            //MessageBox.Show(requeststring, "Request");
            //return false;
            //var serverSettings = new EcrSettings()
            //{
            //    Ip = "87.241.191.72",
            //    Port = 9999,
            //    Password = "MX3HGDZF",
            //    EcrCashier = new EcrCashier(3, "3"),
            //    CashierDepartment = new Department() { Id = 1}

            //};
            //var server = new EcrServer(serverSettings);
            //server.PrintReceiptFromExcelFile(false, true);
            //var product = new Product("10140", "Ջերմուկ ա/շ ո/տ 0․5լ", "հատ") {Price = 125, Qty = 1,};
            //var products = new List<Product> { product };
            //server.PrintReceipt(products, new InvoicePaid() {PaidAmount = 125});
            //var time = 123456789;
            //var date = EcrConvert.ConvertUnixTimestampToDatetime(time);
            //var datetime = DateTime.Now;
            //datetime = date;
            ////time = EcrConvert.ToUnixTimestamp(datetime);
            //time = EcrConvert.ConvertDatetimeToUnixTimestamp(datetime);
            //var manager = new EcrServer(new EcrSettings());
            //manager.GetReport(ReportType.Z);
        }

        static string RandomString()
        {
            System.IO.File.Open(@"C:\Users\HrayrG\AppData\Local\OneMarketData\user.config", System.IO.FileMode.Open);
            //const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];
                var length = 12;
                for (var ind = 0; ind < length; ind++)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    if (ind > 0 && ind % 4 == 0) res.Append("-");
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        private static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Newtonsoft.Json.dll"))
            {
                if (stream == null) return null;
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }



        }
    }
}
