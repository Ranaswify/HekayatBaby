using HekayatBaby.ViewModels;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HekayatBaby.Models
{
    public class Items
    {
        public List<string> imgUrls { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double coast { get; set; }
        public string category { get; set; }
        public string imageUrl { get; set; }
    }
    public class ItemsToSend
    {
        public string name { get; set; }
        public int Count { get; set; }
    }

    public class SavedItems : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Items myItem { get; set; }
        public string documentID { get; set; }
        public string userId { get; set; }
        int _count = 1;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }
        double _totalSellingPrice;
        public double TotalSellingPrice
        {
            get
            {
                _totalSellingPrice = myItem.coast;
                return _totalSellingPrice; 
            }
            set
            {
                _totalSellingPrice = value;
                OnPropertyChanged();
            }
        }
    }
}
