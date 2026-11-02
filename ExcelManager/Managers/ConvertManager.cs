using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TaxServiceInvoices.Helper;

namespace TaxServiceInvoices.Managers
{
    public class ConvertManager
    {
        private Dictionary<int, string> ArmChars = new Dictionary<int, string>();
        private Dictionary<int, string> ArmCharsCustom = new Dictionary<int, string>();
        public ConvertManager()
        {
            InitArmCharsForAsciiAndUnicode();
        }
        private void InitArmCharsForAscii()
        {
            ArmChars.Add(178, "Ա");
            ArmChars.Add(179, "ա");
            ArmChars.Add(180, "Բ");
            ArmChars.Add(181, "բ");
            ArmChars.Add(182, "Գ");
            ArmChars.Add(183, "գ");
            ArmChars.Add(184, "Դ");
            ArmChars.Add(185, "դ");
            ArmChars.Add(186, "Ե");
            ArmChars.Add(187, "ե");
            ArmChars.Add(188, "Զ");
            ArmChars.Add(189, "զ");
            ArmChars.Add(190, "Է");
            ArmChars.Add(191, "է");
            ArmChars.Add(192, "Ը");
            ArmChars.Add(193, "ը");
            ArmChars.Add(194, "Թ");
            ArmChars.Add(195, "թ");
            ArmChars.Add(196, "Ժ");
            ArmChars.Add(197, "ժ");
            ArmChars.Add(198, "Ի");
            ArmChars.Add(199, "ի");
            ArmChars.Add(200, "Լ");
            ArmChars.Add(201, "լ");
            ArmChars.Add(202, "Խ");
            ArmChars.Add(203, "խ");
            ArmChars.Add(204, "Ծ");
            ArmChars.Add(205, "ծ");
            ArmChars.Add(206, "Կ");
            ArmChars.Add(207, "կ");
            ArmChars.Add(208, "Հ");
            ArmChars.Add(209, "հ");
            ArmChars.Add(210, "Ձ");
            ArmChars.Add(211, "ձ");
            ArmChars.Add(212, "Ղ");
            ArmChars.Add(213, "ղ");
            ArmChars.Add(214, "Ճ");
            ArmChars.Add(215, "ճ");
            ArmChars.Add(216, "Մ");
            ArmChars.Add(217, "մ");
            ArmChars.Add(218, "Յ");
            ArmChars.Add(219, "յ");
            ArmChars.Add(220, "Ն");
            ArmChars.Add(221, "ն");
            ArmChars.Add(222, "Շ");
            ArmChars.Add(223, "շ");
            ArmChars.Add(224, "Ո");
            ArmChars.Add(225, "ո");
            ArmChars.Add(226, "Չ");
            ArmChars.Add(227, "չ");
            ArmChars.Add(228, "Պ");
            ArmChars.Add(229, "պ");
            ArmChars.Add(230, "Ջ");
            ArmChars.Add(231, "ջ");
            ArmChars.Add(232, "Ռ");
            ArmChars.Add(233, "ռ");
            ArmChars.Add(234, "Ս");
            ArmChars.Add(235, "ս");
            ArmChars.Add(236, "Վ");
            ArmChars.Add(237, "վ");
            ArmChars.Add(238, "Տ");
            ArmChars.Add(239, "տ");
            ArmChars.Add(240, "Ր");
            ArmChars.Add(241, "ր");
            ArmChars.Add(242, "Ց");
            ArmChars.Add(243, "ց");
            ArmChars.Add(244, "Ւ");
            ArmChars.Add(245, "ւ");
            ArmChars.Add(246, "Փ");
            ArmChars.Add(247, "փ");
            ArmChars.Add(248, "Ք");
            ArmChars.Add(249, "ք");
            ArmChars.Add(250, "Օ");
            ArmChars.Add(251, "օ");
            ArmChars.Add(252, "Ֆ");
            ArmChars.Add(253, "ֆ");
            ArmChars.Add(168, "և");
        }
        private void InitArmCharsForAsciiAndUnicode()
        {
            #region add Ascii codes
            ArmChars.Add(45, "-");
            ArmChars.Add(46, ".");
            ArmChars.Add(47, "/");
            ArmChars.Add(48, "0");
            ArmChars.Add(49, "1");
            ArmChars.Add(50, "2");
            ArmChars.Add(51, "3");
            ArmChars.Add(52, "4");
            ArmChars.Add(53, "5");
            ArmChars.Add(54, "6");
            ArmChars.Add(55, "7");
            ArmChars.Add(56, "8");
            ArmChars.Add(57, "9");
            ArmChars.Add(178, "Ա");
            ArmChars.Add(179, "ա");
            ArmChars.Add(180, "Բ");
            ArmChars.Add(181, "բ");
            ArmChars.Add(182, "Գ");
            ArmChars.Add(183, "գ");
            ArmChars.Add(184, "Դ");
            ArmChars.Add(185, "դ");
            ArmChars.Add(186, "Ե");
            ArmChars.Add(187, "ե");
            ArmChars.Add(188, "Զ");
            ArmChars.Add(189, "զ");
            ArmChars.Add(190, "Է");
            ArmChars.Add(191, "է");
            ArmChars.Add(192, "Ը");
            ArmChars.Add(193, "ը");
            ArmChars.Add(194, "Թ");
            ArmChars.Add(195, "թ");
            ArmChars.Add(196, "Ժ");
            ArmChars.Add(197, "ժ");
            ArmChars.Add(198, "Ի");
            ArmChars.Add(199, "ի");
            ArmChars.Add(200, "Լ");
            ArmChars.Add(201, "լ");
            ArmChars.Add(202, "Խ");
            ArmChars.Add(203, "խ");
            ArmChars.Add(204, "Ծ");
            ArmChars.Add(205, "ծ");
            ArmChars.Add(206, "Կ");
            ArmChars.Add(207, "կ");
            ArmChars.Add(208, "Հ");
            ArmChars.Add(209, "հ");
            ArmChars.Add(210, "Ձ");
            ArmChars.Add(211, "ձ");
            ArmChars.Add(212, "Ղ");
            ArmChars.Add(213, "ղ");
            ArmChars.Add(214, "Ճ");
            ArmChars.Add(215, "ճ");
            ArmChars.Add(216, "Մ");
            ArmChars.Add(217, "մ");
            ArmChars.Add(218, "Յ");
            ArmChars.Add(219, "յ");
            ArmChars.Add(220, "Ն");
            ArmChars.Add(221, "ն");
            ArmChars.Add(222, "Շ");
            ArmChars.Add(223, "շ");
            ArmChars.Add(224, "Ո");
            ArmChars.Add(225, "ո");
            ArmChars.Add(226, "Չ");
            ArmChars.Add(227, "չ");
            ArmChars.Add(228, "Պ");
            ArmChars.Add(229, "պ");
            ArmChars.Add(230, "Ջ");
            ArmChars.Add(231, "ջ");
            ArmChars.Add(232, "Ռ");
            ArmChars.Add(233, "ռ");
            ArmChars.Add(234, "Ս");
            ArmChars.Add(235, "ս");
            ArmChars.Add(236, "Վ");
            ArmChars.Add(237, "վ");
            ArmChars.Add(238, "Տ");
            ArmChars.Add(239, "տ");
            ArmChars.Add(240, "Ր");
            ArmChars.Add(241, "ր");
            ArmChars.Add(242, "Ց");
            ArmChars.Add(243, "ց");
            ArmChars.Add(244, "Ւ");
            ArmChars.Add(245, "ւ");
            ArmChars.Add(246, "Փ");
            ArmChars.Add(247, "փ");
            ArmChars.Add(248, "Ք");
            ArmChars.Add(249, "ք");
            ArmChars.Add(250, "Օ");
            ArmChars.Add(251, "օ");
            ArmChars.Add(252, "Ֆ");
            ArmChars.Add(253, "ֆ");
            ArmChars.Add(168, "և");
            ArmChars.Add(176, "'");
            ArmChars.Add(167, "«");
            
            #endregion
            #region add Unicode codes
            ArmChars.Add(1110, "ա");
            ArmChars.Add(1030, "Ա");
            //ArmChars.Add(1031, "ա");
            ArmChars.Add(1032, "Բ");
            ArmChars.Add(1033, "բ");
            //ArmChars.Add(1034, "Գ");
            //ArmChars.Add(1035, "գ");
            ArmChars.Add(1034, "Դ");
            ArmChars.Add(8470, "դ");
            //ArmChars.Add(1036, "Ե");
            //ArmChars.Add(1037, "ե");
            ArmChars.Add(1036, "Զ");
            ArmChars.Add(1037, "զ");
            ArmChars.Add(1109, "Է");
            ArmChars.Add(1039, "է");
            ArmChars.Add(1040, "Ը");
            ArmChars.Add(1041, "ը");
            ArmChars.Add(1042, "Թ");
            ArmChars.Add(1043, "թ");
            ArmChars.Add(1044, "Ժ");
            ArmChars.Add(1045, "ժ");
            ArmChars.Add(1046, "Ի");
            ArmChars.Add(1047, "ի");
            ArmChars.Add(1048, "Լ");
            ArmChars.Add(1049, "լ");
            ArmChars.Add(1050, "Խ");
            ArmChars.Add(1051, "խ");
            ArmChars.Add(1052, "Ծ");
            ArmChars.Add(1053, "ծ");
            ArmChars.Add(1054, "Կ");
            ArmChars.Add(1055, "կ");
            ArmChars.Add(1056, "Հ");
            ArmChars.Add(1057, "հ");
            ArmChars.Add(1058, "Ձ");
            ArmChars.Add(1059, "ձ");
            ArmChars.Add(1060, "Ղ");
            ArmChars.Add(1061, "ղ");
            ArmChars.Add(1062, "Ճ");
            ArmChars.Add(1063, "ճ");
            ArmChars.Add(1064, "Մ");
            ArmChars.Add(1065, "մ");
            ArmChars.Add(1066, "Յ");
            ArmChars.Add(1067, "յ");
            ArmChars.Add(1068, "Ն");
            ArmChars.Add(1069, "ն");
            ArmChars.Add(1070, "Շ");
            ArmChars.Add(1071, "շ");
            ArmChars.Add(1072, "Ո");
            ArmChars.Add(1073, "ո");
            ArmChars.Add(1074, "Չ");
            ArmChars.Add(1075, "չ");
            ArmChars.Add(1076, "Պ");
            ArmChars.Add(1077, "պ");
            ArmChars.Add(1078, "Ջ");
            ArmChars.Add(1079, "ջ");
            ArmChars.Add(1080, "Ռ");
            ArmChars.Add(1081, "ռ");
            ArmChars.Add(1082, "Ս");
            ArmChars.Add(1083, "ս");
            ArmChars.Add(1084, "Վ");
            ArmChars.Add(1085, "վ");
            ArmChars.Add(1086, "Տ");
            ArmChars.Add(1087, "տ");
            ArmChars.Add(1088, "Ր");
            ArmChars.Add(1089, "ր");
            ArmChars.Add(1090, "Ց");
            ArmChars.Add(1091, "ց");
            ArmChars.Add(1092, "Ւ");
            ArmChars.Add(1093, "ւ");
            ArmChars.Add(1094, "Փ");
            ArmChars.Add(1095, "փ");
            ArmChars.Add(1096, "Ք");
            ArmChars.Add(1097, "ք");
            ArmChars.Add(1098, "Օ");
            ArmChars.Add(1099, "օ");
            ArmChars.Add(1100, "Ֆ");
            ArmChars.Add(1101, "ֆ");
            ArmChars.Add(1102, "և");
            #endregion
        }
        public void ConvertACSI_FromExcelToXml()
        {
            var path = FileManager.OpenExcelFile();
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return; }
            var text = UnicodeAsciiToUnicode(ExcelImportManager.GetDataFormExcel(path));
        }
        public bool ExcelAsciiToUnicodeFromCode(bool isCode = false)
        {
            var path = FileManager.OpenExcelFile("Excel files(*.xls *.xlsx *.xlsm)|*.xls;*.xlsx;*.xlsm|Excel with macros|*.xlsm|Excel 97-2003 file|*.xls", "Select file");
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return false; }
            return ExcelImportManager.ConvertExcelDataToUnicodeFromAscii(path, isCode);
        }
        public bool ExcelUnicodeToAsciiByCode()
        {
            var path = FileManager.OpenExcelFile("Excel files(*.xls *.xlsx *.xlsm)|*.xls;*.xlsx;*.xlsm|Excel with macros|*.xlsm|Excel 97-2003 file|*.xls", "Select file");
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) { return false; }
            return ExcelImportManager.ConvertExcelDataToAsciiCode(path);
        }
        public string UnicodeAsciiToUnicode(string text)
        {
            var result = string.Empty;
            foreach (char chr in text)
            {
                var dec = chr & 0xFF;
                if (dec == 6) dec -= 4;
                var dec1 = (chr >> 8) & 0xFF;
                if (ArmChars.ContainsKey(chr))
                {
                    result += ArmChars[chr];
                }
                else
                {
                    result += chr;
                }
            }
            return result;
        }
        public string AsciiToUnicode(string text)
        {
            var result = string.Empty;
            foreach (char chr in text)
            {
                var dec = chr & 0xFF;
                if (dec == 6) dec -= 4;
                var dec1 = (chr >> 8) & 0xFF;
                if (ArmChars.ContainsKey(chr))
                {
                    result += ArmChars[chr];
                }
                else
                {
                    result += chr;
                }
            }
            return result;
        }

        public byte[] UnicodeToAscii(string text)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(text);
            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    //string encodedValue = "\\u" + ((int)c).ToString("x4");

                    if (ArmChars.ContainsValue(c.ToString()))
                    {
                        var dec = ArmChars.FirstOrDefault(s => s.Value.Contains(c));
                        sb.Append(((char)dec.Key).ToString());
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }
            return Encoding.ASCII.GetBytes(sb.ToString());
        }

        public static string ConvertUnicodeStringToAsciiString(string value)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;
            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(value);
            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            string asciiString = new string(asciiChars);
            return asciiString;
        }
        public string ConvertUnicodeStringToAsciiCode(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    //string encodedValue = "\\u" + ((int)c).ToString("x4");

                    if (ArmChars.ContainsValue(c.ToString()))
                    {
                        var dec = ArmChars.FirstOrDefault(s => s.Value.Contains(c));
                        sb.Append(string.Format("{0}{1}", (dec.Key), ";"));
                    }
                    else
                    {
                        sb.Append(string.Format("{0}{1}", ((int)c), ";"));
                    }
                }
                else
                {
                    sb.Append(string.Format("{0}{1}", (int)(c & 0xFF), ";"));
                }
            }
            return sb.ToString();
        }
        public string ConvertAsciiCodeToString(string value)
        {
            var values = value.Split(';');
            StringBuilder text = new StringBuilder();
            foreach (var txt in values)
            {
                if (string.IsNullOrEmpty(txt)) continue;
                int code;
                int.TryParse(txt, out code);
                if (code <= 0 || code > 255)
                    return value;
                text.Append(ArmChars.ContainsKey(code) ? ArmChars[code] : ((char)code).ToString());
            }
            return text.ToString();
        }
        public string ConvertAsciiToUnicode(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            StringBuilder text = new StringBuilder();
            foreach (var txt in value)
            {
                int code = txt;
                text.Append((code <= 0 || code > 255) ? txt.ToString() : ArmChars.ContainsKey(code) ? ArmChars[code] : ((char)code).ToString());
            }
            return text.ToString();
        }
    }
}
