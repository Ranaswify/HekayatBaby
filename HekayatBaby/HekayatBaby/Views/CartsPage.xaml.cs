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
    public partial class CartsPage : ContentPage
    {
        public CartsPage()
        {
            InitializeComponent();
            BindingContext = new CartsViewModel();
        }
    }
}