using HekayatBaby.Controls;
using HekayatBaby.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HekayatBaby
{
    public partial class App : Application
    {
        const string _Language = "Language";
        public string Language
        {
            get
            {
                if (Preferences.ContainsKey(_Language))
                    return Preferences.Get(_Language, "").ToString();
                return "";
            }
            set
            {
                Preferences.Set(_Language, value);
                //app.Properties[_Language] = value;
            }
        }

        public App()
        {
            InitializeComponent();
            //if (Preferences.Get("UserId", "") != null)
            //{
            //    //MainPage = new CustomNavigationPage(new HomePage());
            //    MainPage = new CustomNavigationPage(new MainPage());

            //}
            //else
            //{
            //    MainPage = new CustomNavigationPage(new LoginPage());
            //}
             MainPage = new CustomNavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
