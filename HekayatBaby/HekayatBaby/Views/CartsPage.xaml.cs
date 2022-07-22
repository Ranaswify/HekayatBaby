using HekayatBaby.Controls;
using HekayatBaby.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HekayatBaby.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartsPage : CustomFoOverrideBackButtonInContentPage
    {
        public CartsPage()
        {
            InitializeComponent();
            BindingContext = new CartsViewModel();
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#f7d0e8");
            navigationPage.BarTextColor = Color.FromHex("#e6306a");
        }
    }
}