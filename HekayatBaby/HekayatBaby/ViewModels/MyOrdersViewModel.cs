using HekayatBaby.Helper;
using HekayatBaby.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class MyOrdersViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ICommand CancelOrderCommand { get; set; }
        public ICommand CheckedCommand { get; set; }
        public ICommand NotCheckedCommand { get; set; }
        public ICommand CloseGridCommand { get; set; }

        ObservableCollection<PaymentInfo> allSaved;
        public ObservableCollection<PaymentInfo> AllSaved
        {
            set
            {
                allSaved = value;
                OnPropertyChanged();
            }
            get
            {
                return allSaved;
            }
        }

        ObservableCollection<string> images;
        public ObservableCollection<string> Images
        {
            set
            {
                images = value;
                OnPropertyChanged();
            }
            get
            {
                return images;
            }
        }

        bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                OnPropertyChanged();
            }
        }
        bool _IsNotChecked;
        public bool IsNotChecked
        {
            get { return _IsNotChecked; }
            set
            {
                _IsNotChecked = value;
                OnPropertyChanged();
            }
        }
        public MyOrdersViewModel()
        {
            CancelOrderCommand = new Command<PaymentInfo>(async (i) => { await CancelOrder(i); });
            CheckedCommand = new Command<PaymentInfo>(async (i) => { await CheckedOrder(i); });
            NotCheckedCommand = new Command(NotCheckedOrder);
            CloseGridCommand = new Command(CloseGrid);

            getAll();

        }

        private void CloseGrid()
        {
            IsNotChecked = false;
        }

        private void NotCheckedOrder()
        {
            IsNotChecked = true;
        }

        private async Task CheckedOrder(PaymentInfo i)
        {
            try
            {
                IsChecked = true;
                await Task.Delay(1000);
                IsChecked = false;
                i.IsChecked = true;
                await firebaseHelper.UpdatePerson(i, i.documentID);
                i.IsDelivered = false;
            }
            catch(Exception e)
            {

            }
            
        }

        private async Task CancelOrder(PaymentInfo i)
        {
            bool res= await Application.Current.MainPage.DisplayAlert("", "Are you sure you want to cancel this order", "Yes", "No");
            if (res)
            {
                IsLoading = true;
                try
                {
                    AllSaved.Remove(i);
                    await CrossCloudFirestore.Current
                             .Instance
                             .Collection("Payment")
                             .Document(i.documentID)
                             .DeleteAsync();
                    IsLoading = false;
                }
                catch (Exception ex)
                {
                    IsLoading = false;
                }
            }
            
        }

        async Task getAll()
        {
            try
            {
                IsLoading = true;
                if (!string.IsNullOrEmpty(Preferences.Get("UserId", "")))
                {
                    AllSaved = await firebaseHelper.GetMyOrders();
                    var i = AllSaved.Where(a => a.UserId == Preferences.Get("UserId", ""));
                    AllSaved = new ObservableCollection<PaymentInfo>(i);
                }

                IsLoading = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }
        }
    }
}
