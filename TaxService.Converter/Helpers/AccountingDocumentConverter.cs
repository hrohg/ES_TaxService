using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using TaxService.Manager.Models;
using TaxServiceInvoices.Enums;
using TaxServiceInvoices.Helper;

namespace TaxService.Manager.Helpers
{
    public class AccountingDocumentConverter
    {
        public static bool ExportSaleAccDocToXml(EsMarket.SharedData.Models.EsMarketInvoice invoice, string path = null)
        {
            if (invoice == null) return false;

            if (string.IsNullOrEmpty(path))
            {
                path = $"S{GetFileName(invoice)}";
            }

            return ExportDataToXml(ConvertToSaleAccDoc(invoice), path);
        }

        protected static ExportedAccDocData ConvertAccountingDocumentFromXml()
        {
            var fileDialog = new OpenFileDialog { Filter = "xml | *.xml" };
            if (fileDialog.ShowDialog() == true)
            {
                return ConvertAccountingDocumentFromXml(fileDialog.FileName);
            }
            return null;
        }

        protected static ExportedAccDocData ConvertAccountingDocumentFromXml(string path)
        {
            return XmlHelper.Load<ExportedAccDocData>(path);
        }

        protected static ExportedSaleAccDocData ConvertToSaleAccDoc(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            if (invoice == null || invoice.GoodsInfo == null) return null;

            try
            {
                return new ExportedSaleAccDocData
                {
                    AccountingDocument = GetSaleAccountingDocument(invoice)
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static SaleAccountingDocument GetSaleAccountingDocument(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            List<Good> goodInfos = invoice.GoodsInfo.Select(s => new Good
            {
                Description = s.Description,
                ClassifierCode = s.HcdCs,
                DealType = EnumHelper.GetEnumName((DealTypes)invoice.InvoiceInfo.DealType),
                Unit = s.Unit,
                Amount = s.Quantity,
                PricePerUnit = s.Price,
                Price = s.Price,
                TotalPrice = s.Total,
            }).ToList();

            return new SaleAccountingDocument
            {
                //Type = invoice.InvoiceInfo != null ? (int)invoice.InvoiceInfo.Type : 0,
                GeneralInfo = GetGeneralInfo(invoice),
                SupplierInfo = GetSupplierInfo(invoice),
                BuyerInfo = GetBuyerInfo(invoice),
                GoodsInfo = new GoodsInfo { Goods = goodInfos },
            };
        }
        private static GeneralInfo GetGeneralInfo(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            return invoice.InvoiceInfo != null && invoice.DeliveryInfo != null ?
                new SaleGeneralInfo()
                {
                    DeliveryDate = string.Format("{0:yyyy-MM-ddK}", invoice.DeliveryInfo.DeliveryDate),
                    Procedure = 1,
                    AdjustmentAccount = false
                }
                : new GeneralInfo();
        }
        private static SupplierInfo GetSupplierInfo(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            return invoice.SupplierInfo != null ?
                new SaleSupplierInfo
                {
                    Taxpayer = new SupplierTaxpayer
                    {
                        TIN = invoice.SupplierInfo.Tin,
                        Name = invoice.SupplierInfo.Name,
                        Address = invoice.SupplierInfo.Address,
                        BankAccount = invoice.SupplierInfo.BankAccount != null ?
                                        new BankAccount
                                        {
                                            BankName = invoice.SupplierInfo.BankAccount.BankName,
                                            BankAccountNumber = invoice.SupplierInfo.BankAccount.BankAccountNumber
                                        } : new BankAccount()
                    },
                    SupplyLocation = invoice.SupplierInfo.SupplyAddress
                }
                : new SupplierInfo();
        }
        private static BuyerInfo GetBuyerInfo(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            return new BuyerInfo
            {
                Taxpayer = invoice.BuyerInfo != null ? new BuyerTaxpayer()
                {
                    TIN = invoice.BuyerInfo.Tin,
                    Name = invoice.BuyerInfo.Name,
                    Address = invoice.BuyerInfo.Address,
                } : new BuyerTaxpayer(),
                DeliveryMethod = invoice.DeliveryInfo != null ? invoice.DeliveryInfo.DeliveryMethod == EsMarket.SharedData.Enums.DeliveryTypeEnum.SelfeDelivery ? "Ինքնատեղափոխում" : "" : "",
                DeliveryLocation = invoice.DeliveryInfo != null && invoice.DeliveryInfo.DeliveryLocation != null ? invoice.DeliveryInfo.DeliveryLocation.Address : "",
            };
        }


        /// <summary>
        /// Stock transfer accounting document
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool ExportStockTransferAccDocToXml(EsMarket.SharedData.Models.EsMarketInvoice invoice, string path = null)
        {
            if (invoice == null) return false;

            if (string.IsNullOrEmpty(path))
            {
                path = $"T{GetFileName(invoice)}";
            }

            return ExportDataToXml(ConvertToStockTransferAccDoc(invoice), path);
        }

        protected static ExportedStockTransferAccDocData ConvertToStockTransferAccDoc(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            if (invoice == null || invoice.GoodsInfo == null) return null;

            try
            {
                return new ExportedStockTransferAccDocData
                {
                    AccountingDocument = GetStockTransferAccountingDocument(invoice)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static StockTransferAccountingDocument GetStockTransferAccountingDocument(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            List<Good> goodInfos = invoice.GoodsInfo.Select(s => new Good
            {
                Description = s.Description,
                ClassifierCode = s.HcdCs,
                Unit = s.Unit,
                Amount = s.Quantity, //.ToString(CultureInfo.InvariantCulture),
                PricePerUnit = s.Price, //.ToString(CultureInfo.InvariantCulture),
                Price = s.Price, //.ToString(CultureInfo.InvariantCulture),
                TotalPrice = s.Total,
            }).ToList();

            return new StockTransferAccountingDocument
            {
                //Type = invoice.InvoiceInfo != null ? (int)invoice.InvoiceInfo.Type : 0,
                GeneralInfo = GetStockTransferGeneralInfo(invoice),
                SupplierInfo = GetStockTransferSupplierInfo(invoice),
                GoodsInfo = new GoodsInfo { Goods = goodInfos },
            };
        }
        private static StockTransferGeneralInfo GetStockTransferGeneralInfo(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            return invoice.InvoiceInfo != null && invoice.DeliveryInfo != null ?
                new StockTransferGeneralInfo()
                {
                    DeliveryDate = string.Format("{0:yyyy-MM-ddK}", invoice.DeliveryInfo.DeliveryDate)
                }
                : new StockTransferGeneralInfo();
        }
        private static StockTransferSupplierInfo GetStockTransferSupplierInfo(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            return invoice.SupplierInfo != null ? new StockTransferSupplierInfo
            {
                Taxpayer = new Taxpayer
                {
                    TIN = invoice.SupplierInfo.Tin,
                    Name = invoice.SupplierInfo.Name,
                    Address = invoice.SupplierInfo.Address
                },
                SupplyLocation = invoice.SupplierInfo.SupplyAddress
            } : new StockTransferSupplierInfo();
        }

        public static bool ExportDataToXml<T>(T data, string fileName)
        {
            if (data == null) return false;

            var fileDialog = new SaveFileDialog { Filter = "xml | *.xml", FileName = fileName };
            if (fileDialog.ShowDialog() != true) return false;

            return XmlHelper.Save(data, fileDialog.FileName);
        }

        private static string GetFileName(EsMarket.SharedData.Models.EsMarketInvoice invoice)
        {
            return invoice != null && invoice.InvoiceInfo != null && !string.IsNullOrEmpty(invoice.InvoiceInfo.InvoiceNumber) ? invoice.InvoiceInfo.InvoiceNumber : Guid.NewGuid().ToString("N").Substring(0, 8);
        }
    }
}
