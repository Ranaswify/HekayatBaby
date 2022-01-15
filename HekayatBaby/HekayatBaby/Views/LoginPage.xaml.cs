using HekayatBaby.Controls;
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
    public partial class LoginPage : CustomFoOverrideBackButtonInContentPage
    {
        NavigationHelper navigationHelper = new NavigationHelper();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new MainPage());
            await navigationHelper.NavigateTo(new NavigationPage(new MainPage()));

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
            //await navigationHelper.NavigateTo(new NavigationPage(new SignUpPage()));
             //Application.Current.MainPage = new CustomNavigationPage(new SignUpPage());

            //await Navigation.PushAsync(new SignInWithPhoneNoPage());

        }
    }
}