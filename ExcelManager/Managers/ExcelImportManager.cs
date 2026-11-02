using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using TaxServiceInvoices.Helper;
using TaxServiceInvoices.Models;
using Constants = TaxServiceInvoices.Helper.Constants;

namespace TaxServiceInvoices.Managers
{
    public class ExcelImportManager
    {
        private const string ExcelFileExtensions = "Excel files(*.xls *.xlsx *․xlsm)|*.xls;*.xlsm;*․xlsx|Excel with macros|*.xlsm|Excel 97-2003 file|*.xls";
        public static Tuple<InvoiceModel, List<InvoiceItemsModel>> ImportSaleInvoice(string filePath = null,  bool addVAT = true)
        {
            var file = !string.IsNullOrEmpty(filePath)? filePath: FileManager.OpenExcelFile("Excel files(*.xls *.xlsx *․xlsm)|*.xls;*.xlsm;*․xlsx|Excel with macros|*.xlsm|Excel 97-2003 file|*.xls", "Excel ֆայլի բեռնում");
            if (file == null) return null;
            using (var xlApp = new ExcelDataContent(file))
            {
                var xlWSh = xlApp.GetWorksheet();
                if (xlWSh == null) return null;
                var invoice = new InvoiceModel();
                var invoiceItems = new List<InvoiceItemsModel>();
                try
                {
                    xlWSh.Activate();
                    invoice.ProviderName = xlWSh.Cells[1, 3].Text;
                    invoice.RecipientName = xlWSh.Cells[5, 6].Text;
                    invoice.InvoiceNumber = xlWSh.Cells[2, 7].Text;
                    invoice.CreateDate = HgConvert.ToDateTime(xlWSh.Cells[7, 5].Text);
                    var nextRowIndex = 16;

                    var nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    while (!string.IsNullOrEmpty(nextCode))
                    {
                        var invoiceItem = new InvoiceItemsModel();
                        invoiceItem.Code = xlWSh.Cells[nextRowIndex, 2].Text;
                        invoiceItem.HcdCs = null;
                        invoiceItem.Description = xlWSh.Cells[nextRowIndex, 3].Text;
                        invoiceItem.Mu = xlWSh.Cells[nextRowIndex, 4].Text;
                        invoiceItem.Quantity = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 5].Text);
                        invoiceItem.Price = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 6].Text);
                        invoiceItem.Price += addVAT ? invoiceItem.Price * 20 / 100 : 0;
                        invoiceItem.HcdCs = xlWSh.Cells[nextRowIndex, 8].Text;
                        invoiceItem.Note = xlWSh.Cells[nextRowIndex, 8].Text;
                        invoiceItems.Add(invoiceItem);
                        nextRowIndex++;
                        nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    }
                    xlApp.GetWorkbook().Close(false);
                    invoice.Summ = invoiceItems.Sum(s => s.Price * s.Quantity);
                    return new Tuple<InvoiceModel, List<InvoiceItemsModel>>(invoice, invoiceItems);
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    xlApp.Dispose();
                }
            }
        }
        public static InvoiceModel GetInvoice(string path, bool addVAT = true)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }
            using (var xlApp = new ExcelDataContent(path))
            {
                var xlWSh = xlApp.GetWorksheet();
                if (xlWSh == null) return null;
                var invoice = new InvoiceModel();
                var invoiceItems = new List<InvoiceItemsModel>();
                try
                {
                    xlWSh.Activate();
                    invoice.CreateDate = HgConvert.ToDateTime(xlWSh.Cells[7, 5].Text);
                    invoice.SupplierInfo.Taxpayer = new Taxpayer
                    {
                        Name = invoice.ProviderName = xlWSh.Cells[1, 3].Text,
                        Address = invoice.ProviderAddress = xlWSh.Cells[2, 3].Text,
                        TIN = xlWSh.Cells[3, 3].Text
                    };
                    invoice.SupplierInfo.SupplyLocation = invoice.ProviderAddress = xlWSh.Cells[5, 3].Text;
                    invoice.SupplierInfo.ConveyLocation = invoice.ProviderAddress = xlWSh.Cells[10, 3].Text;
                    invoice.SupplierInfo.ConveyMethod = invoice.ProviderAddress = xlWSh.Cells[11, 3].Text;
                    invoice.GeneralInfo.DeliveryDate = HgConvert.ToDateTime(invoice.ProviderName = xlWSh.Cells[10, 5].Text);
                    invoice.RecipientName = xlWSh.Cells[5, 3].Text;
                    invoice.InvoiceNumber = xlWSh.Cells[2, 7].Text;

                    var nextRowIndex = 16;

                    var nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    while (!string.IsNullOrEmpty(nextCode))
                    {
                        var invoiceItem = new InvoiceItemsModel();
                        invoiceItem.Code = xlWSh.Cells[nextRowIndex, 2].Text;
                        invoiceItem.Description = xlWSh.Cells[nextRowIndex, 3].Text;
                        invoiceItem.Mu = xlWSh.Cells[nextRowIndex, 4].Text;
                        invoiceItem.Quantity = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 5].Text);
                        invoiceItem.Price = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 6].Text);
                        invoiceItem.Price += addVAT ? invoiceItem.Price * 20 / 100 : 0;
                        invoiceItem.Note = xlWSh.Cells[nextRowIndex, 8].Text;
                        invoiceItems.Add(invoiceItem);
                        nextRowIndex++;
                        nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    }
                    xlApp.GetWorkbook().Close(false);
                    invoice.Summ = invoiceItems.Sum(s => s.Price * s.Quantity);
                    invoice.InvoiceItems = invoiceItems;
                    return invoice;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    xlApp.Dispose();
                }
            }
        }
        public static AccountingDocument GetTaxServiceInvoice(string path, bool addVAT = true)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }
            using (var xlApp = new ExcelDataContent(path))
            {
                var xlWSh = xlApp.GetWorksheet();
                if (xlWSh == null) return null;
                var invoice = new AccountingDocument((int)Constants.AccountingDocumentType.TexapoxmanAccountingDocument);
                var invoiceItems = new List<GoodInfos>();
                try
                {
                    xlWSh.Activate();
                    invoice.CreateDate = HgConvert.ToDateTime(xlWSh.Cells[7, 5].Text);
                    invoice.SupplierInfo.Taxpayer = new Taxpayer
                    {
                        Name = xlWSh.Cells[1, 3].Text,
                        Address = xlWSh.Cells[2, 3].Text,
                        TIN = xlWSh.Cells[3, 3].Text
                    };
                    invoice.SupplierInfo.SupplyLocation = xlWSh.Cells[5, 3].Text;
                    invoice.SupplierInfo.ConveyLocation = xlWSh.Cells[10, 3].Text;
                    invoice.SupplierInfo.ConveyMethod = xlWSh.Cells[11, 3].Text;
                    invoice.GeneralInfo.DeliveryDate = string.IsNullOrEmpty(xlWSh.Cells[11, 5].Text) ? DateTime.Today : HgConvert.ToDateTime(xlWSh.Cells[11, 5].Text);

                    var nextRowIndex = 16;

                    var nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    while (!string.IsNullOrEmpty(nextCode))
                    {
                        var invoiceItem = new GoodInfos();
                        invoiceItem.Code = xlWSh.Cells[nextRowIndex, 2].Text;
                        invoiceItem.Description = xlWSh.Cells[nextRowIndex, 3].Text;
                        invoiceItem.Unit = xlWSh.Cells[nextRowIndex, 4].Text;
                        invoiceItem.Amount = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 5].Text);
                        invoiceItem.Price = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 6].Text);
                        invoiceItem.Price += addVAT ? invoiceItem.Price * 20 / 100 : 0;
                        invoiceItem.PricePerUnit = invoiceItem.Price;
                        invoiceItem.Note = xlWSh.Cells[nextRowIndex, 8].Text;
                        invoiceItems.Add(invoiceItem);
                        nextRowIndex++;
                        nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    }
                    xlApp.GetWorkbook().Close(false);
                    invoice.Total = invoiceItems.Sum(s => s.TotalPrice);
                    invoice.GoodsInfo = invoiceItems;
                    return invoice;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    xlApp.Dispose();
                }
            }
        }

        public static string GetDataFormExcel(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }
            using (var xlApp = new ExcelDataContent(path))
            {
                var xlWSh = xlApp.GetWorksheet();
                if (xlWSh == null) return null;

                try
                {
                    xlWSh.Activate();
                    //var invoice = new AccountingDocument((int)Constants.AccountingDocumentType.TexapoxmanAccountingDocument);
                    //var invoiceItems = new ObservableCollection<Good>();
                    //invoice.CreateDate = HgConvert.ToDateTime(xlWSh.Cells[7, 5].Text);
                    //invoice.SupplierInfo.Taxpayer = new Taxpayer
                    //{
                    //    Name = xlWSh.Cells[1, 3].Text,
                    //    Address = xlWSh.Cells[2, 3].Text,
                    //    TIN = xlWSh.Cells[3, 3].Text
                    //};
                    //invoice.SupplierInfo.SupplyLocation = xlWSh.Cells[5, 3].Text;
                    //invoice.SupplierInfo.ConveyLocation = xlWSh.Cells[10, 3].Text;
                    //invoice.SupplierInfo.ConveyMethod = xlWSh.Cells[11, 3].Text;
                    //invoice.GeneralInfo.DeliveryDate = string.IsNullOrEmpty(xlWSh.Cells[11, 5].Text)?DateTime.Today: HgConvert.ToDateTime(xlWSh.Cells[11, 5].Text);

                    //var nextRowIndex = 16;

                    //var nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    //while (!string.IsNullOrEmpty(nextCode))
                    //{
                    //    var invoiceItem = new Good();
                    //    invoiceItem.Code = xlWSh.Cells[nextRowIndex, 2].Text;
                    //    invoiceItem.Description = xlWSh.Cells[nextRowIndex, 3].Text;
                    //    invoiceItem.Unit = xlWSh.Cells[nextRowIndex, 4].Text;
                    //    invoiceItem.Amount = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 5].Text);
                    //    invoiceItem.Price = HgConvert.ToDecimal(xlWSh.Cells[nextRowIndex, 6].Text);

                    //    invoiceItem.PricePerUnit = invoiceItem.Price;
                    //    invoiceItem.Note = xlWSh.Cells[nextRowIndex, 8].Text;
                    //    invoiceItems.Add(invoiceItem);
                    //    nextRowIndex++;
                    //    nextCode = xlWSh.Cells[nextRowIndex, 2].Text;
                    //}
                    //invoice.Total = invoiceItems.Sum(s => s.TotalPrice);
                    //invoice.GoodsInfo = invoiceItems;
                    var text = xlWSh.Cells[29, 5].Text;
                    xlApp.GetWorkbook().Close(false);

                    return text;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    xlApp.Dispose();
                }
            }
        }

        public static bool ConvertExcelDataToAsciiCode(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return false;
            }
            using (var xlApp = new ExcelDataContent(path))
            {
                var xlWb = xlApp.GetWorkbook();
                var xlWSh = xlWb.ActiveSheet;
                if (xlWSh == null) return false;
                Worksheet xlWShExported = xlWb.Worksheets.Add(After: xlWSh);
                try
                {
                    xlWSh.Activate();
                    var range = xlWSh.UsedRange;
                    var converter = new ConvertManager();
                    for (int r = 1; r <= range.Rows.Count; r++)
                    {
                        for (int c = 1; c <= range.Columns.Count; c++)
                        {
                            if (xlWSh.Cells[r, c] == null) continue;
                            var value = ((Range)xlWSh.Cells[r, c]).Value2;

                            if (value == null) continue;
                            
                            var convertedValue = value is string? converter.ConvertUnicodeStringToAsciiCode(value.ToString()):value;
                            ((Range)xlWShExported.Cells[r, c]).Value = convertedValue;
                            //if (value is string)
                            //{
                            //    //var ss = ConvertManager.ConvertUnicodeStringToAsciiString(value);
                            //   // var convertedValue = converter.UnicodeToAscii(value);
                            //    //var text = converter.AsciiToUnicode(convertedValue);
                            //    var convertedValue = converter.ConvertUnicodeStringToAsciiCode(value);
                            //    ((Range)xlWShExported.Cells[r, c]).Value = convertedValue;
                            //}
                            //else
                            //{
                            //    ((Range)xlWShExported.Cells[r, c]).Value2 = value;
                            //}

                        }

                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    xlApp.GetWorkbook().Close(true);
                    xlApp.Dispose();
                }
            }
            return true;
        }
        public static bool ConvertExcelDataToUnicodeFromAscii(string path, bool isCode=false)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return false;
            }
            using (var xlApp = new ExcelDataContent(path))
            {
                var xlWb = xlApp.GetWorkbook();
                var xlWSh = xlWb.ActiveSheet;
                if (xlWSh == null) return false;
                Worksheet xlWShExported = xlWb.Worksheets.Add(After: xlWSh);
                try
                {
                    xlWSh.Activate();
                    var range = xlWSh.UsedRange;
                    var converter = new ConvertManager();
                    for (int r = 1; r <= range.Rows.Count; r++)
                    {
                        for (int c = 1; c <= range.Columns.Count; c++)
                        {
                            if (xlWSh.Cells[r, c] == null) continue;
                            var value = ((Range)xlWSh.Cells[r, c]).Value2;

                            if (value == null) continue;

                            var convertedValue = value is string ? (isCode ? converter.ConvertAsciiCodeToString(value.ToString()) : converter.ConvertAsciiToUnicode(value.ToString())) : value;
                            ((Range)xlWShExported.Cells[r, c]).Value = convertedValue;
                        }

                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    xlApp.GetWorkbook().Close(true);
                    xlApp.Dispose();
                }
            }
            return true;
        }
        public static bool ConvertExcelDataToUnicodeFromAsciiCode(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return false;
            }
            using (var xlApp = new ExcelDataContent(path))
            {
                var xlWb = xlApp.GetWorkbook();
                var xlWSh = xlWb.ActiveSheet;
                if (xlWSh == null) return false;
                Worksheet xlWShExported = xlWb.Worksheets.Add(After: xlWSh);
                try
                {
                    xlWSh.Activate();
                    var range = xlWSh.UsedRange;
                    var converter = new ConvertManager();
                    for (int r = 1; r <= range.Rows.Count; r++)
                    {
                        for (int c = 1; c <= range.Columns.Count; c++)
                        {
                            if (xlWSh.Cells[r, c] == null) continue;
                            var value = ((Range)xlWSh.Cells[r, c]).Value2;

                            if (value == null) continue;

                            var convertedValue = value is string ? converter.ConvertAsciiCodeToString(value.ToString()) : value;
                            ((Range)xlWShExported.Cells[r, c]).Value = convertedValue;
                        }

                    }

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    xlApp.GetWorkbook().Close(true);
                    xlApp.Dispose();
                }
            }
            return true;
        }

    }
}
