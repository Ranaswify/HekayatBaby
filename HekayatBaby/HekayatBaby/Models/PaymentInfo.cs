using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace HekayatBaby.Models
{
    public class PaymentInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
        public string PromoCode { get; set; }
        public string OrderStatus { get; set; }
        public double PaidAmount { get; set; }
        public ItemsToPay MyItemsToPay { get; set; }
        public DateTime CreatedTime { get; set; }
        public string documentID { get; set; }
        public bool IsChecked { get; set; }

        bool _isProccess;
        public bool IsProccess
        {
            get
            {
                if (OrderStatus == "Processing")
                { _isProccess=true; }
                else
                {
                    _isProccess = false;
                }
                return _isProccess;

            }
            set
            {
                _isProccess = value;
                OnPropertyChanged();
            }
        }

        bool _isDelivered;
        public bool IsDelivered
        {
            get
            {
                if (OrderStatus == "Delivered" && !IsChecked)
                { _isDelivered = true; }
                else
                {
                    _isDelivered = false;
                }
                return _isDelivered;

            }
            set
            {
                _isDelivered = value;
                OnPropertyChanged();
            }
        }
    }

    public class ItemsToPay : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<SavedItems> ItemToPay { get; set; }
        double totalAmount;
        public double TotalAmount
        {
            set
            {
                totalAmount = value;
                OnPropertyChanged();
            }
            get
            {
                return totalAmount;
            }
        }

    }
}