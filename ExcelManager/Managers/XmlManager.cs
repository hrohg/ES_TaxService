using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TaxServiceInvoices.Helper;
using TaxServiceInvoices.Models;

namespace TaxServiceInvoices.Managers
{
    namespace ESL.Common.Helpers
    {
        public class XmlManager
        {
            public string GetConfigFilePath
            {
                get { return string.Format("{0}{1}{2}", ConfigFileConstants.FilePath, @"\", ConfigFileConstants.FileName); }
            }

            #region Public Methods

            public XDocument GetDocument(string path = null)
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = GetConfigFilePath;
                }
                return XDocument.Load(ConfigFileConstants.FileName);
            }
            public static XmlDocument GetNewDocument()
            {
                return new XmlDocument();
            }
            public static void SetInvoice(AccountingDocument invoice)
            {
                var xmlDoc = GetNewDocument();
                var root = xmlDoc.CreateNode(XmlNodeType.Element, XmlConstants.TaxServiceInvoices.Type, "");
                var generalInfo = xmlDoc.CreateNode(XmlNodeType.Element, XmlConstants.TaxServiceInvoices.Type, "");

                var i = Serialize(invoice);
            }
            public static void SetTaxServiceInvoice(AccountingDocument invoice)
            {
                if (invoice == null) { return; }
                var i = Serialize(invoice);
                ObjectXmlSerializer.Save(invoice, "");
                var xmlDoc = GetNewDocument();
                var root = xmlDoc.CreateNode(XmlNodeType.Element, XmlConstants.TaxServiceInvoices.Type, "");
                var generalInfo = xmlDoc.CreateNode(XmlNodeType.Element, XmlConstants.TaxServiceInvoices.Type, "");
            }
            public static XDocument GetTaxServiceInvoice(AccountingDocument invoice)
            {
                if (invoice == null) { return null; }
                var xDocument = new XDocument();
                try
                {
                    xDocument.Add(new XElement("ExportedAccDocData",new XAttribute("xmlns", "http://www.taxservice.am/tp3/invoice/definitions")));
                    var xmlDocument = new ObjectToXmlSerializer().SerializeObject(invoice);
                    xDocument.Root.Add(XElement.Parse(xmlDocument.OuterXml));
                    var element = xDocument.Descendants("AccountingDocument").Last();
                    element.SetAttributeValue("Version", "1.0");
                    var xmlElementTotal = GetElement("<Total><TotalPrice>0</TotalPrice></Total>");
                     element = xDocument.Descendants("Good").Last();
                    element.AddAfterSelf(xmlElementTotal);
                }
                catch (Exception ex)
                {
                    return null;
                }
                return xDocument;
            }
            private static XElement GetElement(string text)
            {
                return XElement.Parse(text);
            }
            public static string Serialize<T>(T value)
            {
                if (value == null)
                {
                    return string.Empty;
                }
                try
                {
                    var xmlserializer = new XmlSerializer(typeof(T));
                    var stringWriter = new StringWriter();
                    using (var writer = XmlWriter.Create(stringWriter))
                    {
                        xmlserializer.Serialize(writer, value);
                        return stringWriter.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred" + ex.Message, "An error occurred XmlSerializer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
            public string GetElementInnerText(string element)
            {
                try
                {
                    var xDoc = GetDocument();
                    var root = xDoc.Elements(XmlConstants.TaxServiceInvoices.ExportedAccDocData);
                    var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
                    if (xEl != null)
                    {
                        return xEl.Value;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return null;
            }
            public bool SetElementInnerText(string value, string element)
            {
                try
                {
                    var xDoc = GetDocument();
                    var root = xDoc.Elements(XmlConstants.TaxServiceInvoices.ExportedAccDocData);
                    var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
                    if (xEl != null)
                    {
                        xEl.Value = value;
                        xDoc.Save(ConfigFileConstants.FileName);
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }


            //public List<XmlSettingsItem> GetItemsByControl(string element)
            //{
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var root = xDoc.Elements(XmlTagItems.Root);
            //        var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
            //        if (xEl == null)
            //        {
            //            return new List<XmlSettingsItem>();
            //        }
            //        var items = xEl.Elements().Select(el => new XmlSettingsItem()
            //        {
            //            Key = el.Attribute(XmlSettingsItem.KeyProperty).Value,
            //            Data = el.Attribute(XmlSettingsItem.DataProperty).Value,
            //            Value = el.Attribute(XmlSettingsItem.ValueProperty).Value,
            //            Member = el.Attribute(XmlSettingsItem.MemberProperty).Value
            //        }).ToList();
            //        return items;
            //    }
            //    catch (Exception)
            //    {
            //        return new List<XmlSettingsItem>();
            //    }
            //}
            //public bool SetStockItemsByControl(List<XmlSettingsItem> items, string element)
            //{
            //    if (string.IsNullOrEmpty(element))
            //    {
            //        return false;
            //    }
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var root = xDoc.Elements(XmlTagItems.Root);
            //        var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
            //        if (xEl != null)
            //        {
            //            xEl.RemoveAll();
            //            foreach (var item in items)
            //            {
            //                var stock = new XElement(item.Key);
            //                stock.SetAttributeValue(XmlSettingsItem.KeyProperty, item.Key);
            //                stock.SetAttributeValue(XmlSettingsItem.DataProperty, item.Data);
            //                stock.SetAttributeValue(XmlSettingsItem.ValueProperty, item.Value);
            //                stock.SetAttributeValue(XmlSettingsItem.MemberProperty, item.Member);
            //                xEl.Add(stock);
            //            }
            //            xDoc.Save(_FileName);
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }

            //}
            //public bool SetStockItemsByControl<T>(List<T> items, string element)
            //{
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var root = xDoc.Elements("Settings");
            //        var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
            //        if (xEl != null)
            //        {
            //            xEl.RemoveAll();
            //            var property = items.First().GetType();
            //            foreach (var item in items)
            //            {

            //            }
            //            var stock = new XElement("Stock");
            //            stock.SetAttributeValue("Key", 122);
            //            xEl.Add(stock);
            //            xDoc.Save(_FileName);
            //        }

            //        //root.AppendChild(stocks);
            //        //
            //        //var doc = new XmlDocument();
            //        //doc.Load("ESLConf.Xml");
            //        //var node = doc.GetElementsByTagName(control);
            //        //doc.DocumentElement.SetAttribute("Stock","Key","aa");
            //        //using (FileStream stream = File.OpenWrite("ESLConf.Xml"))
            //        //{
            //        //    //stream.
            //        //    //;
            //        //}
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }
            //    return true;
            //}
            //public bool SetStockItemsByControl<T>(T o, string control)
            //{
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var root = xDoc.Elements("Settings");
            //        var xEl = root.Descendants().FirstOrDefault(s => s.Name == control);
            //        if (xEl != null)
            //        {
            //            var stock = new XElement("Stock");
            //            stock.SetAttributeValue("Key", 120);
            //            xEl.Add(stock);
            //            xDoc.Save(_FileName);
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }
            //    return true;
            //}
            //public bool SetItems(List<XmlSettingsItem> items, string element)
            //{
            //    if (string.IsNullOrEmpty(element))
            //    {
            //        return false;
            //    }
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var root = xDoc.Elements(XmlTagItems.Root);
            //        var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
            //        if (xEl != null)
            //        {
            //            xEl.RemoveAll();
            //            foreach (var item in items)
            //            {
            //                var stock = new XElement(item.Key);
            //                stock.SetAttributeValue(XmlSettingsItem.KeyProperty, item.Key);
            //                stock.SetAttributeValue(XmlSettingsItem.DataProperty, item.Data);
            //                stock.SetAttributeValue(XmlSettingsItem.ValueProperty, item.Value);
            //                stock.SetAttributeValue(XmlSettingsItem.MemberProperty, item.Member);
            //                xEl.Add(stock);
            //            }
            //            xDoc.Save(_FileName);
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }

            //}
            //public bool InsertItems(List<XmlSettingsItem> items, string element)
            //{
            //    if (string.IsNullOrEmpty(element))
            //    {
            //        return false;
            //    }
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var root = xDoc.Elements(XmlTagItems.Root);
            //        var xEl = root.Descendants().FirstOrDefault(s => s.Name == element);
            //        if (xEl != null)
            //        {
            //            foreach (var item in items)
            //            {
            //                var stock = new XElement(item.Key);
            //                stock.SetAttributeValue(XmlSettingsItem.KeyProperty, item.Key);
            //                stock.SetAttributeValue(XmlSettingsItem.DataProperty, item.Data);
            //                stock.SetAttributeValue(XmlSettingsItem.ValueProperty, item.Value);
            //                stock.SetAttributeValue(XmlSettingsItem.MemberProperty, item.Member);
            //                xEl.Add(stock);
            //            }
            //            xDoc.Save(_FileName);
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return false;
            //    }

            //}
            //public List<XmlSettingsItem> GetItemsFromRootByControl(string root, string element)
            //{
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var rootPath = xDoc.Elements(XmlTagItems.Root);
            //        var rootEl = rootPath.Descendants().FirstOrDefault(s => s.Name == root);
            //        if (rootEl == null) { return new List<XmlSettingsItem>(); }
            //        var xEl = rootEl.Descendants().FirstOrDefault(s => s.Name == element);
            //        if (xEl == null)
            //        {
            //            return new List<XmlSettingsItem>();
            //        }
            //        var items = xEl.Elements().Select(el => new XmlSettingsItem()
            //        {
            //            Key = el.Attribute(XmlSettingsItem.KeyProperty).Value,
            //            Data = el.Attribute(XmlSettingsItem.DataProperty).Value,
            //            Value = el.Attribute(XmlSettingsItem.ValueProperty).Value,
            //            Member = el.Attribute(XmlSettingsItem.MemberProperty).Value
            //        }).ToList();
            //        return items;
            //    }
            //    catch (Exception)
            //    {
            //        return new List<XmlSettingsItem>();
            //    }
            //}
            //public List<XmlSettingsItem> GetItemsByControl(string root, string element)
            //{
            //    try
            //    {
            //        var xDoc = XDocument.Load(_FileName);
            //        var rootPath = xDoc.Elements(XmlTagItems.Root);
            //        var rootEl = rootPath.Descendants().FirstOrDefault(s => s.Name == root);
            //        if (rootEl == null) { return new List<XmlSettingsItem>(); }
            //        var xmlEl = rootEl.Elements().Where(s => s.Name == element).ToList();
            //        var items = new List<XmlSettingsItem>();
            //        foreach (var xEl in xmlEl)
            //        {
            //            var newItem = new XmlSettingsItem
            //            {
            //                Key = xEl.Attribute(XmlSettingsItem.KeyProperty) != null ? xEl.Attribute(XmlSettingsItem.KeyProperty).Value : string.Empty,
            //                Data = xEl.Attribute(XmlSettingsItem.DataProperty) != null ? xEl.Attribute(XmlSettingsItem.DataProperty).Value : string.Empty,
            //                Value = xEl.Attribute(XmlSettingsItem.ValueProperty) != null ? xEl.Attribute(XmlSettingsItem.ValueProperty).Value : string.Empty,
            //                Member = xEl.Attribute(XmlSettingsItem.MemberProperty) != null ? xEl.Attribute(XmlSettingsItem.MemberProperty).Value : string.Empty
            //            };
            //            foreach (var xElement in xEl.Elements())
            //            {
            //                newItem.Elements.Add(xElement);
            //            }
            //            items.Add(newItem);
            //        }
            //        return items;
            //    }
            //    catch (Exception)
            //    {
            //        return new List<XmlSettingsItem>();
            //    }
            //}
            //public object GetElementValueByControl(string root, string element)
            //{
            //    var item = GetItemsFromRootByControl(root, element).FirstOrDefault();
            //    if (item == null)
            //    {
            //        return string.Empty;
            //    }
            //    return item.Value;
            //}
            //public object GetElementsByControl(string element)
            //{
            //    return GetItemsByControl(element);
            //}
            #endregion
        }



    }

}
