using HekayatBaby.Helper;
using HekayatBaby.Models;
using HekayatBaby.Views;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    class ItemDetailsViewModel : BaseViewModel
    {
       
        private Items selectedItem;

        List<string> allOrders;
        public List<string> AllOrders
        {
            set
            {
                allOrders = value;
                OnPropertyChanged();
            }
            get
            {
                return allOrders;
            }
        }

        string description;
        public string Description
        {
            set
            {
                description = value;
                OnPropertyChanged();
            }
            get
            {
                return description;
            }
        }

        string coast;
        public string Coast
        {
            set
            {
                coast = value;
                OnPropertyChanged();
            }
            get
            {
                return coast;
            }
        }

        Items order;
        public Items Order
        {
            set
            {
                order = value;
                OnPropertyChanged();
            }
            get
            {
                return order;
            }
        }
        public ICommand PluCommand { get; set; }
        public ICommand MinusCommand { get; set; }
        public ICommand GoToPaymentCommand { get; set; }

        double count = 1;
        public double Count
        {
            set
            {
                count = value;
                OnPropertyChanged();
            }
            get
            {
                return count;
            }
        }

        public ItemDetailsViewModel(Items selectedItem)
        {
            this.selectedItem = selectedItem;
            Order = selectedItem;
            //PluCommand = new Command(PlusPressed);
            MinusCommand = new Command(async () => await MinusPressed());
            GoToPaymentCommand = new Command(async () => await GoToPayment());
          
            AllOrders = selectedItem.imgUrls;
            Description = selectedItem.description;
            Coast = selectedItem.coast.ToString();
        }

        private async Task GoToPayment()
        {
            try
            {
                SavedItems savedItems = new SavedItems()
                {
                    myItem = Order,
                    userId = Preferences.Get("UserId", "")
                };
                var i = CrossCloudFirestore.Current
                                      .Instance
                                      .Collection("Carts")
                                      .AddAsync(savedItems)
                                      .ContinueWith(t =>
                                      {
                                          System.Diagnostics.Debug.WriteLine(t.Exception);
                                      }, TaskContinuationOptions.OnlyOnFaulted);
                
               await Application.Current.MainPage.Navigation.PushAsync(new CartsPage());

            }
            catch (Exception ex)
            {

            }
           

        }

        private async Task MinusPressed()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartsPage());

        }

        public ItemDetailsViewModel()
        {

        }
        //private void PlusPressed()
        //{
        //    MainThread.BeginInvokeOnMainThread(() =>
        //    {
        //        if (Count >= 1)
        //        {
        //            Count++;
        //            var i = Convert.ToDouble(Coast);
        //            Coast = (i * Count).ToString();

        //        }
        //    });
        //}
    }
}
