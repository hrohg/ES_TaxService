using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TaxServiceInvoices.Helper;
using TaxServiceInvoices.Managers.ESL.Common.Helpers;
using TaxServiceInvoices.Models;

namespace TaxServiceInvoices.Managers
{
    public class InvoiceManager
    {
        public bool ConvertExcelToAccountingDocument()
        {
            var path = FileManager.OpenExcelFile();
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) {return false;}
            var invocie = GetTaxServiceInvoiceFromExcel(path);
            return ExportInvoiceToXml(invocie, string.Format(@"{0}\{1}.xml",Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)));
        }
        public bool ConvertExcelToAccountingDocument(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return false; }
            var invocie = GetTaxServiceInvoiceFromExcel(path);
            return ExportInvoiceToXml(invocie, string.Format(@"{0}\{1}.xml", Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)));
        }
        public AccountingDocument GetTaxServiceInvoiceFromExcel(string path)
        {
            var invoice = ExcelImportManager.GetTaxServiceInvoice(path, true);
            return invoice;
        }
        public bool ExportInvoiceToXml(AccountingDocument invoice, string path)
        {
            var invoiceInXDoc = XmlManager.GetTaxServiceInvoice(invoice);
            try
            {
                ConvertToXml(invoiceInXDoc).Save(path);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool ExportInvoiceToXml(AccountingDocument invoice)
        {
            XmlManager.GetTaxServiceInvoice(invoice);
            return true;
        }
        public XmlDocument ConvertToXml(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var reader = xDocument.CreateReader())
            {
                xmlDocument.Load(reader);
            }

            var xDeclaration = xDocument.Declaration;
            if (xDeclaration != null)
            {
                var xmlDeclaration = xmlDocument.CreateXmlDeclaration(
                    xDeclaration.Version,
                    xDeclaration.Encoding,
                    xDeclaration.Standalone);

                xmlDocument.InsertBefore(xmlDeclaration, xmlDocument.FirstChild);
            }

            return xmlDocument;
        } 
    }
}
