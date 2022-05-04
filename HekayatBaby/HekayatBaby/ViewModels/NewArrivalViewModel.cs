using HekayatBaby.Helper;
using HekayatBaby.Models;
using HekayatBaby.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class NewArrivalViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private string v;
        ObservableCollection<Items> allOrders;
        public ObservableCollection<Items> AllOrders
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

        ObservableCollection<Items> allToys;
        public ObservableCollection<Items> AllToys
        {
            set
            {
                allToys = value;
                OnPropertyChanged();
            }
            get
            {
                return allToys;
            }
        }
        ObservableCollection<string> categores;
        public ObservableCollection<string> Categores
        {
            set
            {
                categores = value;
                OnPropertyChanged();
            }
            get
            {
                return categores;
            }
        }
      
        bool _IsSorting;
        public bool IsSorting
        {
            get { return _IsSorting; }
            set
            {
                _IsSorting = value;
                OnPropertyChanged();
            }
        }


        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        string _SelectedFilter;
        public string SelectedFilter
        {
            get { return _SelectedFilter; }
            set
            {
                _SelectedFilter = value;
                OnPropertyChanged();
            }
        }

        Items _SelectedItem;
        public Items SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetDetailsCommand { get; set; }
        public ICommand GoToCartsCommand { get; set; }
        public ICommand GoToHomeCommand { get; set; }
        public ICommand SortingCommand { get; set; }
        public ICommand SelectedFilterCommand { get; set; }
        public ICommand FilterHighToLowCommand { get; set; }
        public ICommand FilterLowToHighCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand CloseSortCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand GoToMyOrdersCommand { get; set; }
        public NewArrivalViewModel(string v)
        {
            this.v = v;
            AllOrders = new ObservableCollection<Items>();
            Categores = new ObservableCollection<string>();
            GetDetailsCommand = new Command(async () => await GoToDetails());
            GoToCartsCommand = new Command(async () => await GoToCarts());
            GoToHomeCommand = new Command(async () => await GoToHome());
            SortingCommand = new Command(Sorting);
            SelectedFilterCommand = new Command(async () => await SelectFilter());
            FilterLowToHighCommand = new Command(FilterLowToHigh);
            FilterHighToLowCommand = new Command(FilterHighToLow);
            SearchCommand = new Command(SearchForToy);
            CloseSortCommand = new Command(CloseSorting);
            RefreshCommand = new Command(async () => await RefreshItems());
            GoToMyOrdersCommand = new Command(async () => await GoToMyOrders());
            getAll(v);
            GetCategores();
            Title = v;
        }

        private async Task GoToMyOrders()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new MyOrdersPage());
        }

        private async Task RefreshItems()
        {
           await getAll(v);
        }

        private void CloseSorting()
        {
            IsSorting = false;
        }

        private async Task GoToHome()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        private async Task GoToCarts()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CartsPage());
        }
        private void SearchForToy()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var filteredItems = AllOrders.Where(name => name.name.ToLower().Contains(Name.ToLower())).ToList();
                AllOrders = new ObservableCollection<Items>(filteredItems);
            }
            else
            {
                AllOrders = AllToys;
            }
        }

        private void FilterHighToLow()
        {
            var i = AllOrders.OrderByDescending(x => x.coast).ToList();
            AllOrders = new ObservableCollection<Items>(i);
            IsSorting = false;
        }

        private void FilterLowToHigh()
        {
            var i = AllOrders.OrderBy(x => x.coast).ToList();
            AllOrders = new ObservableCollection<Items>(i);
            IsSorting = false;
        }

        public NewArrivalViewModel()
        {
            AllOrders = new ObservableCollection<Items>();
            GetDetailsCommand = new Command(async () => await GoToDetails());
            SelectedFilterCommand = new Command(async () => await SelectFilter());

            //getAll(v);
        }

        private async Task SelectFilter()
        {
            if (SelectedFilter != null)
            {
                List<Items> i = AllOrders.Where(a => a.category == SelectedFilter).ToList();
                AllOrders = new ObservableCollection<Items>(i);
                IsSorting = false;
                SelectedFilter = null;
            }
        }
        private void GetCategores()
        {
            Categores.Add("Girls");
            Categores.Add("Boys");
            Categores.Add("Education");
            Categores.Add("Clothes");
            Categores.Add("Entertaining");
            //Categores.Add("Price (Low To high)");
            //Categores.Add("Price (High To Low)");
        }
        private void Sorting()
        {
            IsSorting = !IsSorting;
        }

        async Task GoToDetails()
        {
            if (SelectedItem != null)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new ItemDetails(SelectedItem));
                SelectedItem = null;
            }

        }

        async Task getAll(string dbName)
        {
            IsLoading = true;
            AllOrders = await firebaseHelper.GetAllItems(dbName);
            AllToys = AllOrders;
            foreach(var i in AllOrders)
            {
                i.imageUrl = i.imgUrls[0];
            }
            IsLoading = false;
        }

    }
}
