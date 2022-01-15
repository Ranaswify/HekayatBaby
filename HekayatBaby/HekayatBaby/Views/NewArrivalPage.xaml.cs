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
    public partial class NewArrivalPage : CustomFoOverrideBackButtonInContentPage
    {
        private string v;
        public NewArrivalPage() 
        {
            InitializeComponent();
           // var navigationPage = Application.Current.MainPage as NavigationPage;
            //navigationPage.BarBackgroundColor = Color.Red;
            //navigationPage.BarTextColor = Color.White;
        }

        public NewArrivalPage(string v)
        {
            InitializeComponent();
            this.v = v;
            BindingContext = new NewArrivalViewModel(v);
            var navigationPage = Application.Current.MainPage as NavigationPage;
            navigationPage.BarBackgroundColor = Color.FromHex("#f7d0e8");
            navigationPage.BarTextColor = Color.FromHex("#e6306a");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}