using System;
using System.ComponentModel;
using System.Windows.Input;
using TaxService.Manager.Enums;
using TaxService.Manager.Helpers;
using TaxServiceInvoices.Managers;

namespace TaxService.Manager.Models
{
    class ConverterViewModel : INotifyPropertyChanged
    {
        #region Internal properties
        private string _log;
        #endregion //Internal properties

        #region External properties
        public string Description { get; set; }
        public string ConvertAsciiToUnicodeDescription { get; set; }
        public string Log
        {
            get { return _log; }
            set
            {
                if(_log==value) return;
                _log = value;
                OnPropertyChanged("Log");
            }
        }

        #endregion //External properties

        #region Constructors

        public ConverterViewModel()
        {
            Description = "You can invert your data using one of existing converters.\nYou can find your converter in bottom.";
            ConvertAsciiToUnicodeDescription = "Convert data from unicode to ASCII and back in same file.";
            InitializeCommands();
        }
        #endregion //Constructors

        #region Internal methods
        private bool CanConvertExcelAsciiToUnicodeByCode(object o)
        {
            return o is ConvertTo;
        }
        private void OnConvertExcelAsciiToUnicodeByCode(object o)
        {
            switch ((ConvertTo)o)
            {
                case ConvertTo.ExcelAsciiToUnicodeFromCode:
                    Log = "Please wait. Converting from Ascii To Unicode ...";
                    Log = new ConvertManager().ExcelAsciiToUnicodeFromCode(true) ? "Convert from Ascii code To Unicode completed." : "Convert from Ascii code To Unicode Aborted.";
                    break;
                case ConvertTo.ExcelUnicodeToAsciiByCode:
                    Log = "Please wait. Converting from Unicode To Ascii ...";
                    Log = new ConvertManager().ExcelUnicodeToAsciiByCode() ? "Convert from Unicode To Ascii completed" : "Convert from Unicode To Ascii aborted.";
                    break;
                case ConvertTo.ExcelAsciiToUnicode:
                                        Log = "Please wait. Converting from Ascii To Unicode ...";
                    Log = new ConvertManager().ExcelAsciiToUnicodeFromCode(false) ? "Convert from Ascii To Unicode completed." : "Convert from Ascii To Unicode Aborted.";
                    break;

                    break;
                default:
                    throw new ArgumentOutOfRangeException("Command argument", o, null);
            }
        }

        private void InitializeCommands()
        {
            ConvertExcelAsciiToUnicodeByCodeCommand = new RelayCommand(OnConvertExcelAsciiToUnicodeByCode, CanConvertExcelAsciiToUnicodeByCode);
        }

        #endregion //Internal methods

        #region External methods

        #endregion

        #region Commands

        public ICommand ConvertExcelAsciiToUnicodeByCodeCommand { get; set; }

        #endregion //Commands

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
