using HekayatBaby.Helper;
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
    public partial class SignUpPage : ContentPage
    {

        public SignUpPage()
        {
            InitializeComponent();

            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //navigationPage.BarBackgroundColor = Color.LightYellow;
            //navigationPage.BarTextColor = Color.White;
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#f7d0e8");
            navigationPage.BarTextColor = Color.FromHex("#e6306a");
        }
    }
}