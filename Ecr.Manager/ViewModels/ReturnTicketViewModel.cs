using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CashReg.Adapters;
using CashReg.Helper;
using CashReg.Interfaces;
using CashReg.Managers;
using CashReg.Models;
using Ecr.Manager.Helpers;

namespace Ecr.Manager.ViewModels
{
    public class ReturnTicketViewModel : ViewModelBase
    {
        #region Internal property
        private bool _isReturnPartly;
        private string crn;
        private string receiptId;
        private IEcrSettings EcrSettings;
        private EcrReturnTicketAdapter adapter;
        #endregion Internal property

        #region Expernal properties
        public string ReceiptId
        {
            get => receiptId; set
            {
                receiptId = value;
                TicketData = null;
                TicketProductDatas.Clear();
                RaisePropertyChanged(() => IsActive);
            }
        }
        public string Crn { get => crn; set { crn = value; RaisePropertyChanged(() => IsActive); } }

        public bool IsReturnPartly
        {
            get => _isReturnPartly; set
            {
                _isReturnPartly = value;
                RaisePropertyChanged(() => IsReturnPartly);
            }
        }
        public bool IsActive { get => !string.IsNullOrEmpty(Crn) && !string.IsNullOrEmpty(ReceiptId); }
        public bool HasTicketData { get => TicketData != null; }

        public ReturnReceiptResponseModel TicketData { get; set; }
        public ObservableCollection<TicketProductData> TicketProductDatas { get; private set; }

        #endregion External properties

        public ReturnTicketViewModel(IEcrSettings ecrSettings, string crn, string ticketNumber = null)
        {
            EcrSettings = ecrSettings;
            Crn = crn;

            TicketProductDatas = new ObservableCollection<TicketProductData>();
            TicketProductDatas.CollectionChanged += OnTicketProductDatasChanged;
            GetTicketDataCommand = new RelayCommand(OnGetTicketData, o => IsActive);
            ReturnTicketDataCommand = new RelayCommand(OnReturnTicket, o => IsActive && TicketData != null && (!_isReturnPartly || TicketData != null && TicketProductDatas.Where(s => s.IsReturn && s.Quantity > 0).Any()));
        }

        private void OnTicketProductDatasChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        private void OnError(string message)
        {
            MessageManager.ShowMessage(message, "Return ticket");
        }
        private void OnException(Exception ex)
        {
            OnError(ex.Message);
        }
        private void OnReturnTicket(object obj)
        {
            if (TicketData == null) return;

            try
            {
                adapter = new EcrReturnTicketAdapter(EcrSettings, OnError, OnException);

                if (_isReturnPartly)
                {
                    var paid = new EcrPaid { PaidAmount = double.Parse(TicketData.cash), PaidAmountCard = double.Parse(TicketData.card), PrePaymentAmount = double.Parse(TicketData.ppu) };
                    if (!TicketProductDatas.Any(s => s.IsReturn))
                    {
                        MessageManager.ShowMessage("Որևէ ապրանք ընտրված չէ:", "Ետ վերադարձ", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    adapter.PrintReceiptReturnTicket((int)TicketData.rseq, TicketProductDatas.Where(t => t.IsReturn).Select(t => new ReturnItem(int.Parse(t.rpid), double.Parse(t.qty))).ToArray(), paid);
                }
                else
                {
                    adapter.PrintReceiptReturnTicket((int)TicketData.rseq);
                }

                MessageManager.ShowMessage(adapter.Result.Description, "ՀԴՄ կտրոնի վերադարձ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageManager.ShowMessage(ex.Message, "ՀԴՄ կտրոնի վերադարձի սխալ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                adapter.Dispose();
            }
        }

        #region Internal methods        

        private void OnGetTicketData(object obj)
        {
            TicketProductDatas.Clear();
            string result = string.Empty;

            try
            {
                adapter = new EcrReturnTicketAdapter(EcrSettings, OnError, OnException);
                TicketData = adapter.GetReceiptReturnTicket(ReceiptId, Crn);
            }
            catch (Exception ex)
            {
                MessageManager.ShowMessage(ex.Message, "ՀԴՄ կտրոնի վերադարձ");
            }
            finally
            {
                if (null != adapter)
                {
                    result = adapter.Result.Description;
                    adapter.Dispose();
                }
            }

            //Fake data for test
            //TicketData = new ReturnReceiptResponseModel
            //{
            //    totals = new TotalModel[] { new TotalModel { gc = "aa" }, new TotalModel { gc = "cc", p = "112", qty = "22" } }
            //};

            if (TicketData != null)
            {
                var ticketDatas = TicketData.totals != null ?
                    TicketData.totals.Select(s => new TicketProductData
                    {
                        rpid = s.rpid,
                        gc = s.gc,
                        adg = s.adg,
                        gn = s.gn,
                        mu = s.mu,
                        qty = s.qty,
                        p = s.p,
                        did = s.did
                    }).ToList() :
                new List<TicketProductData>();
                foreach (var ticketData in ticketDatas)
                    TicketProductDatas.Add(ticketData);
            }
            else
            {
                MessageManager.ShowMessage(result, "Վերադարձի կտրոնի ստացում", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            RaisePropertyChanged(() => TicketData);
            RaisePropertyChanged(() => HasTicketData);
        }
        #endregion Internal methods

        #region External methods
        #endregion External methods

        #region Commands
        public ICommand CloseCommand { get; private set; }
        public ICommand GetTicketDataCommand { get; private set; }
        public ICommand ReturnTicketDataCommand { get; private set; }
        #endregion Commands
    }

    public class TicketProductData : TotalModel
    {
        public bool IsReturn { get; set; }
    }
}
