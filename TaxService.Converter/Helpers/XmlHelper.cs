using System;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace TaxService.Manager.Helpers
{
    public class XmlHelper
    {
        public static bool Save<T>(T obj, string filePath)
        {
            if (obj == null || string.IsNullOrEmpty(filePath)) return false;

            FileStream file = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    OmitXmlDeclaration = true, Indent = true
                };

                file = !(File.Exists(filePath)) ? File.OpenWrite(filePath) : File.Create(filePath);
                XmlWriter xw = XmlWriter.Create(file, settings);
                xs.Serialize(xw, obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (file != null) file.Close();
                return false;
            }
            finally
            {
                if (file != null) file.Close();
            }
            return true;
        }
        public static T Load<T>(string filePath)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                var item = (T)serializer.Deserialize(fileStream);
                fileStream.Close();
                return item;
            }
            catch (Exception)
            {
                if (fileStream != null) fileStream.Close();

                return default(T);
            }
        }
    }
}
