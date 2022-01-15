using HekayatBaby.Models;
using HekayatBaby.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HekayatBaby.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentPage : ContentPage
    {
        private ItemsToPay itemsToPay;
        public PaymentPage()
        {
            InitializeComponent();
        }

        public PaymentPage(ItemsToPay itemsToPay)
        {
            InitializeComponent();
            this.itemsToPay = itemsToPay;
            BindingContext = new PaymentViewModel(itemsToPay);

        }
    }
}