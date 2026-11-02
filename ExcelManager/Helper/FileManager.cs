using System;
using System.Windows.Forms;

namespace TaxServiceInvoices.Helper
{
    public class FileManager
    {
        private const string InitialDirectory = @"C:\";

        private const string ExcelFilter = "Excel files(*.xls *.xlsx *.xlsm)|*.xls;*.xlsx;*.xlsm|Excel with macros|*.xlsm|Excel 97-2003 file|*.xls";

        public static string OpenFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Մուտքագրման ակտի բեռնում",
                Filter = "All files |*.*",
                InitialDirectory = InitialDirectory
            };
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }
        public static string[] OpenFiles()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Մուտքագրման ակտի բեռնում",
                Filter = "All files |*.*",
                InitialDirectory = InitialDirectory
            };
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            return openFileDialog.FileNames;
        }
        public static string OpenExcelFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select file",
                InitialDirectory = InitialDirectory
            };
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }
        [STAThread]
        public static string OpenExcelFile(string filter, string title)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                InitialDirectory = InitialDirectory
            };
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }
        [STAThread]
        public static string OpenExcelFile(string filter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Excel ֆայլի բեռնում",
                Filter = filter,
                InitialDirectory = InitialDirectory
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;}
            return openFileDialog.FileName;
        }
    }
    
}
