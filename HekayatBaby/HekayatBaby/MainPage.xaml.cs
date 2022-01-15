using HekayatBaby.Controls;
using HekayatBaby.Resources;
using HekayatBaby.Views;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.OpenWhatsApp;

namespace HekayatBaby
{
    public partial class MainPage : MasterDetailPage
    {
        private App app
        {
            get => Application.Current as App;
        }

        public string UserName { get; set; }
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new CustomNavigationPage((Page)Activator.CreateInstance(typeof(HomePage)))
            {
                BarBackgroundColor = Color.FromHex("#f7d0e8"),
                BarTextColor = Color.FromHex("#e6306a")
            };
            UserName = Preferences.Get("UserName", "");

            IsPresented = false;
        }
        private async void OnFacebookPageClicked(object sender, EventArgs e)
        {
            GridIndicator.IsVisible = Indicator.IsRunning = false;

            await Browser.OpenAsync("https://www.facebook.com/%D8%AD%D9%83%D8%A7%D9%8A%D8%A7%D8%AA-%D8%A8%D9%8A%D8%A8%D9%8A-105203128617136/");

        }

        private async void OnFacebookGroupClicked(object sender, EventArgs e)
        {
            GridIndicator.IsVisible = Indicator.IsRunning = false;

            await Browser.OpenAsync("https://www.facebook.com/groups/2248061131913700");

        }

        private async void OnWhatsAppClicked(object sender, EventArgs e)
        {
            try
            {
                GridIndicator.IsVisible = Indicator.IsRunning = false;

                await Launcher.OpenAsync("https://wa.me/+201069896902?text=");
            }
            catch (Exception ex)
            {

            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var SelectedLangauge = await DisplayActionSheet(LanguageResource.ChooosePreferedLangauge, LanguageResource.Cancel, "", "العربية", "English");
            if (SelectedLangauge != null && SelectedLangauge != LanguageResource.Cancel)
            {
                if (SelectedLangauge == "English")
                {
                    app.Language = "en";
                }
                else if (SelectedLangauge == "العربية")
                {
                    app.Language = "ar-EG";
                }
                else
                {
                    app.Language = "en";
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    var culture = new CultureInfo(app.Language);
                    LanguageResource.Culture = culture;
                    CrossMultilingual.Current.CurrentCultureInfo = culture;
                    GridIndicator.IsVisible = Indicator.IsRunning = false;
                    Application.Current.MainPage = new CustomNavigationPage(new MainPage());

                });
            }
        }

        private void Logout_Tapped(object sender, EventArgs e)
        {
            Preferences.Remove("UserId");
            Preferences.Remove("UserName");
            Application.Current.MainPage = new CustomNavigationPage(new LoginPage());
        }
    }
}
