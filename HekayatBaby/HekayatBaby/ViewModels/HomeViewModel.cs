using HekayatBaby.Models;
using HekayatBaby.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using HekayatBaby.Helper;


namespace HekayatBaby.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        NavigationHelper navigationHelper = new NavigationHelper();

        
        public ICommand GetNewArrivalToysCommand { get; set; }
        public ICommand GetBestSellerToysCommand { get; set; }
        public HomeViewModel()
        {
            GetNewArrivalToysCommand = new Command(async () => await GetNewArrivalToys());
            GetBestSellerToysCommand = new Command(async () => await GetBestSellerToys());
        }

        async Task GetNewArrivalToys()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewArrivalPage("NewArrival"));
        }
        async Task GetBestSellerToys()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewArrivalPage("BestSeller"));
        }
    }
}
