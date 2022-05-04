using HekayatBaby.Helper;
using HekayatBaby.Models;
using HekayatBaby.Views;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class CartsViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ICommand GoToPaymentCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand PlusCommand { get; set; }
        public ICommand MinusCommand { get; set; }


        ObservableCollection<SavedItems> allSaved;
        public ObservableCollection<SavedItems> AllSaved
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

        double totalAmount;
        public double TotalAmount
        {
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
            get
            {
                return totalAmount;
            }
        }
        bool isEmpty;
        public bool IsEmpty
        {
            set
            {
                isEmpty = value;
                OnPropertyChanged();
            }
            get
            {
                return isEmpty;
            }
        }
        public CartsViewModel()
        {
            AllSaved = new ObservableCollection<SavedItems>();
            GoToPaymentCommand = new Command(async () => await GoToPayment());
            RemoveItemCommand = new Command<SavedItems>(RemoveItem);
            PlusCommand = new Command<SavedItems>(PlusItem);
            MinusCommand = new Command<SavedItems>(MinusItem);
            getAll();
        }

        private void MinusItem(SavedItems obj)
        {
            if (obj.Count > 1)
            {
                --obj.Count;
                obj.TotalSellingPrice = obj.myItem.coast * obj.Count;
                obj.myItem.coast = obj.TotalSellingPrice;
                SubTotalAmount();
            }
        }
        private void PlusItem(SavedItems obj)
        {
            if (obj.Count >= 1)
            {
                ++obj.Count;
                //double i= obj.myItem.coast * obj.Count;
                obj.TotalSellingPrice = obj.myItem.coast * obj.Count;
                obj.myItem.coast = obj.TotalSellingPrice;
                SubTotalAmount();
            }
        }
        private void SubTotalAmount()
        {
            TotalAmount = 0;
            foreach (var i in AllSaved)
            {
                i.TotalSellingPrice = i.myItem.coast;
                TotalAmount += i.TotalSellingPrice;

            }
        }

        //private void Tar7TotalAmount()
        //{
        //    foreach (var i in AllSaved)
        //    {
        //        TotalAmount -= i.TotalSellingPrice;
        //    }
        //}

        private async Task GoToPayment()
        {
            IsLoading = true;
            ItemsToPay itemsToPay = new ItemsToPay()
            {
                TotalAmount = TotalAmount,
                ItemToPay = AllSaved
            };
            await Application.Current.MainPage.Navigation.PushAsync(new PaymentPage(itemsToPay));
            IsLoading = false;
        }

        private async void RemoveItem(SavedItems i)
        {
            IsLoading = true;
            try
            {
                AllSaved.Remove(i);
                await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Carts")
                         .Document(i.documentID)
                         .DeleteAsync();
                IsLoading = false;
            }
            catch(Exception ex)
            {
                IsLoading = false;
            }
        }
        async Task getAll()
        {
            try
            {
                IsLoading = true;
                if(!string.IsNullOrEmpty(Preferences.Get("UserId", "")))
                {
                    AllSaved = await firebaseHelper.GetAllSavedItems();
                    var i = AllSaved.Where(a => a.userId == Preferences.Get("UserId", ""));
                    AllSaved = new ObservableCollection<SavedItems>(i);
                }

                if (AllSaved.Count > 0)
                {
                    IsEmpty = true;
                }
                SubTotalAmount();
                IsLoading = false;
            }
            catch(Exception ex)
            {
                IsLoading = false;
            }
        }
    }
}