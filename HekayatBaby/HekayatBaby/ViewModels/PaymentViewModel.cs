using HekayatBaby.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {

        private ObservableCollection<SavedItems> allSaved;
        private ItemsToPay itemsToPay;

        string fullName;
        public string FullName
        {
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
            get
            {
                return fullName;
            }
        }

        string notes;
        public string Notes
        {
            set
            {
                notes = value;
                OnPropertyChanged();
            }
            get
            {
                return notes;
            }
        }

        string phoneNo;
        public string PhoneNo
        {
            set
            {
                phoneNo = value;
                OnPropertyChanged();
            }
            get
            {
                return phoneNo;
            }
        }

        string address;
        public string Address
        {
            set
            {
                address = value;
                OnPropertyChanged();
            }
            get
            {
                return address;
            }
        }

        ItemsToPay myItemsToPay;
        public ItemsToPay MyItemsToPay
        {
            set
            {
                myItemsToPay = value;
                OnPropertyChanged();
            }
            get
            {
                return myItemsToPay;
            }
        }

        public ICommand ConfirmPaymentCommand { get; set; }

        public PaymentViewModel()
        {

        }

        public PaymentViewModel(ItemsToPay itemsToPay)
        {
            this.itemsToPay = itemsToPay;
            MyItemsToPay = itemsToPay;
            ConfirmPaymentCommand = new Command(async () => await ConfirmPayment());
        }

        private async Task ConfirmPayment()
        {
            try
            {
                PaymentInfo paymentInfo = new PaymentInfo()
                {
                    PhoneNo = PhoneNo,
                    Address = Address,
                    FullName = FullName,
                    Note = Notes,
                    MyItemsToPay = itemsToPay
                };
                var i = CrossCloudFirestore.Current
                                         .Instance
                                         .Collection("Payment")
                                         .AddAsync(paymentInfo)
                                         .ContinueWith(t =>
                                         {
                                             System.Diagnostics.Debug.WriteLine(t.Exception);
                                         }, TaskContinuationOptions.OnlyOnFaulted);
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}
