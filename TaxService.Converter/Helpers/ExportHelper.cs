using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace TaxService.Manager.Helpers
{
    public class ExportHelper
    {
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
                //MessageBox.Show("An error occurred" + ex.Message, "An error occurred XmlSerializer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }
}
