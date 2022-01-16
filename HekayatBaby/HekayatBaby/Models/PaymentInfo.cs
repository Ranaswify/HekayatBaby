using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HekayatBaby.Models
{
    public class PaymentInfo
    {
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string PromoCode { get; set; }
        public double PaidAmount { get; set; }
        public ItemsToPay MyItemsToPay { get; set; }

    }

    public class ItemsToPay
    {
        public double TotalAmount { get; set; }
        public ObservableCollection<SavedItems> ItemToPay { get; set; }

    }
}
