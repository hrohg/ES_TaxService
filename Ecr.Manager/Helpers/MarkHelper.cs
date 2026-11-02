using Ecr.Manager.Views;
using System.Collections.Generic;

namespace Ecr.Manager.Helpers
{
    public class MarkHelper
    {
        public static List<string> GetEmarkExciseList()
        {
            return new List<string>
            { 
                // Մսամթերքի և կենդանական էքստրակտներ 01-06-2025
                "0207","0209","0210",
                "1601", "1603",

                // Կաթնամթերք և կաթնային հիմքով արտադրատեսակներ 01-06-2025
                "0401", "0402","0403","0404","0405", "0406",
                "2105",

                // Թեյ և սուրճ
                "0901","0902","0903",
                "2101",

                // Կենդանական ու բուսական ճարպ և յուղ 01-03-2025
                "1504","1505", "1506", "1507", "1508","1509","1510","1512","1515","1516","1517",

                // Պահածոյացված մթերք 01-03-2025
                // Ծովամթերք 01-03-2025
                "1602","1604", "1605", 
                "2001", "2002","2003","2004","2005","2006","2007","2008",

                 // Շոկոլադ և կակաո պարունակող այլ պատրաստի սննդամթերք 01-03-2025
                "1806",

                // Մակարոնեղեն և այլ պատրաստի մթերք 01-03-2025
                "1901","1902", "1903", "1904", 
                "2209",

                //Փաթեթավորված խմելու ջուր 01-05-2024
                "2201",
                
                //Գազավորված և ոչ ալկոհոլային ըմպելիքներ 01-03-2025
                //Կաթնային ըմպելիքներ 01-03-2025
                // Գարեջուր և գարեջրային ըմպելիքներ 01-05-2025    
                "2009",
                "2202",
                "2203",

                //Ալկոհոլային արտադրանք 15-10-2023
                "2204", "2205", "2206", "2207", "2208",

                // Կենդանիների կեր 01-06-2025
                "2309",

                // Ծխախոտի արտադրանք 15-03-2023
                "2402", 
                // Ծխախոտի արտադրանք 01-01-2025
                "2404",

                // Դյուրակիր համակարգիչներ և հեռախոսներ
                "8471", 
                "8517",

                // Քսայուղեր և այլ յուղեր 01-09-2025
                "2710",
                "3403",

                // Վիտամիններ 01-01-2026
                "2936",

                //Կոսմետիկա և հիգիենայի միջոցներ, օճառներ, լվացող և մաքրող միջոցներ 01-01-2026
                // Կենդանիների պատվաստանյութ 01-01-2026
                "3002","3004","3006",

                // Ներկանյութեր և լաքեր 01-09-2025
                // Օծանելիք և հարդաջուր 01-01-2026
                "3203","3204","3205","3206","3207","3208","3209","3210","3211","3212",                

                //Կոսմետիկա և հիգիենայի միջոցներ, օճառներ, լվացող և մաքրող միջոցներ 01-01-2026
                "3304","3305","3306","3307",
                "3401","3402",
            };
        }

        public static string GetBarcode(string emark)
        {
            if (string.IsNullOrEmpty(emark) || emark.Length < 25) return emark;
            else if (emark.StartsWith("000000")) return emark.Substring(6, 8);
            else if (emark.StartsWith("01")) return emark.Substring(3, 13);
            else if (emark.StartsWith("0")) return emark.Substring(1, 13);
            return emark;
        }

        public static string GetSerialNumber(string emark)
        {
            if (!string.IsNullOrEmpty(emark) || emark.Length < 25) return emark;
            else if (emark.StartsWith("01")) return emark.Substring(19, 7);
            else if (emark.StartsWith("0")) return emark.Substring(18, 7);
            return emark;
        }
        public static string GetCheckNumber(string emark)
        {
            if (!string.IsNullOrEmpty(emark) || emark.Length < 31) return emark;
            else if (emark.StartsWith("01")) return emark.Substring(28, 4);
            else if (emark.StartsWith("0")) return emark.Substring(27, 4);
            return emark;
        }

        public static string ReadEmark(string description, string emark = null)
        {
            var inputWindow = new InputBox(description) { Title = "Հսկիչ նշանի ընթերցում", InputValue = emark };
            inputWindow.ShowDialog();
            if (inputWindow.DialogResult != true) return null;
            emark = inputWindow.InputValue;
            return emark;
        }
    }
}
