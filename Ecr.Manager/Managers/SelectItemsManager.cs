using CashReg.Helper;
using Ecr.Manager.ViewModels;
using Ecr.Manager.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ecr.Manager.Managers
{
    public static class SelectItemsManager
    {
        public static List<PaymentSystemModel> SelectItem(this List<PaymentSystem> paymentSystems, bool allowMultipleSelection = false)
        {
            List<PaymentSystemModel> result = null;

            List<PaymentSystemModel> paymentSystemModels = paymentSystems.Select(s => new PaymentSystemModel(s.code, s.name, GetLogoPath(s.code))).ToList();

            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    result = SeletPaymentModel(paymentSystemModels);
                });
            }
            else
            {
                result = SeletPaymentModel(paymentSystemModels);
            }
            return result;
        }
        private static List<PaymentSystemModel> SeletPaymentModel(List<PaymentSystemModel> paymentSystemModels)
        {
            var window = new SelectItemsWindow(paymentSystemModels);
            if (window.ShowDialog() != true)
            {
                return null;
            }

            return window.ViewModel.GetSelectedItems().ToList();
        }
        private static string GetLogoPath(int code)
        {
            switch (code)
            {
                case 1:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/MasterCard.png";
                case 10:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Telcell.png";
                //case 11:
                //    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/EasyPay.png";
                case 12:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Mobidram.png";
                case 13:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Idram.png";
                //case 14:
                //    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/StackProcessing.png";
                case 15:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Fastshift.png";
                case 16:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Teampay.png";
                //case 17:
                //    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Payx.png";
                case 18:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Inecopay.png";
                default:
                    return "pack://application:,,,/Ecr.Manager;component/Resources/Images/PaymentSystems/Unknown.png";
            }
        }
    }
}
