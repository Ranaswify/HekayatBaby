using HekayatBaby.Controls;
using HekayatBaby.Helper;
using HekayatBaby.Models;
using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace HekayatBaby.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {

        private ObservableCollection<SavedItems> allSaved;
        private ItemsToPay itemsToPay;
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        string fullName;
        public string FullName
        {
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
            get
            {
                return fullName;
            }
        }

        string notes;
        public string Notes
        {
            set
            {
                notes = value;
                OnPropertyChanged();
            }
            get
            {
                return notes;
            }
        }

        string phoneNo;
        public string PhoneNo
        {
            set
            {
                phoneNo = value;
                OnPropertyChanged();
            }
            get
            {
                return phoneNo;
            }
        }

        string address;
        public string Address
        {
            set
            {
                address = value;
                OnPropertyChanged();
            }
            get
            {
                return address;
            }
        }

        string promoCode;
        public string PromoCode
        {
            set
            {
                promoCode = value;
                OnPropertyChanged();
            }
            get
            {
                return promoCode;
            }
        }

        string promoCodeValue;
        public string PromoCodeValue
        {
            set
            {
                promoCodeValue = value;
                OnPropertyChanged();
            }
            get => promoCodeValue;
        }

        double totalAmountValue;
        public double TotalAmountValue
        {
            set
            {
                totalAmountValue = value;
                OnPropertyChanged();
            }
            get => totalAmountValue;
        }
        double shipping = 50;
        public double Shipping
        {
            set
            {
                shipping = value;
                OnPropertyChanged();
            }
            get => shipping;
        }

        ItemsToPay myItemsToPay;
        public ItemsToPay MyItemsToPay
        {
            set
            {
                myItemsToPay = value;
                OnPropertyChanged();
            }
            get
            {
                return myItemsToPay;
            }
        }

        public ICommand ConfirmPaymentCommand { get; set; }
        public ICommand CheckPromoCommand { get; set; }

        ObservableCollection<MyOrders> myAllItems;
        public ObservableCollection<MyOrders> MyAllItems
        {
            set
            {
                myAllItems = value;
                OnPropertyChanged();
            }
            get
            {
                return myAllItems;
            }
        }


        public PaymentViewModel()
        {

        }
        async Task SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                List<ItemsToSend> itemsToSends = new List<ItemsToSend>();
                for(int i = 0; i < MyItemsToPay.ItemToPay.Count; i++)
                {
                    itemsToSends.Add(new ItemsToSend() { name = MyItemsToPay.ItemToPay[i].myItem.name, Count = MyItemsToPay.ItemToPay[i].Count });
                }
                var list = JsonConvert.SerializeObject(itemsToSends);
                mail.From = new MailAddress("AboutTheKids7@gmail.com");
                mail.To.Add("AboutTheKids7@gmail.com");
                mail.Subject = "New Order";
                mail.Body = "You've recieved a new order" + "\n" +
                    "User Info: " + "\n" +
                    "Name: " + FullName + "\n" + "Address: " + Address + "\n" + "Note: " + Notes + "\n" + "Phone number: " + PhoneNo
                    + "\n" + "Order Info:" + "\n" + "Total Amount: " + TotalAmountValue + "\n" + "Items: " + "\n" + list;
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("AboutTheKids7@gmail.com", "jupkycqfronejjax");

                ServicePointManager.ServerCertificateValidationCallback = delegate (object senders, X509Certificate certificate, X509Chain chain, SslPolicyErrors ssl)
                { return true; };

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                //DisplayAlert("Faild", ex.Message, "OK");
            }
        }

        public PaymentViewModel(ItemsToPay itemsToPay)
        {
            this.itemsToPay = itemsToPay;
            MyItemsToPay = itemsToPay;
            ConfirmPaymentCommand = new Command(async () => await ConfirmPayment());
            CheckPromoCommand = new Command(async () => await CheckPromo());
            TotalAmountValue = MyItemsToPay.TotalAmount + Shipping;
            FullName = Preferences.Get("UserName", "");
            PhoneNo = Preferences.Get("PhoneNo", "");
        }

        private async Task CheckPromo()
        {
            try
            {
                var i = await firebaseHelper.GetAllPromo();
                PromoCode promoCode = i.Where(y => y.name == PromoCode).FirstOrDefault();
                
                if (promoCode != null)
                {
                    double totalValue = MyItemsToPay.TotalAmount;
                    PromoCodeValue = promoCode.value + " %";
                    TotalAmountValue = totalValue - (MyItemsToPay.TotalAmount * (Convert.ToDouble(promoCode.value) / 100));
                    TotalAmountValue += Shipping;
                }
                else
                {
                    PromoCode = "";
                    PromoCodeValue = "";
                    TotalAmountValue = MyItemsToPay.TotalAmount + Shipping;
                    await Application.Current.MainPage.DisplayAlert("", "Promo code is not valid", "Ok");
                }
            }
            catch
            {

            }
           
        }

        private async Task RemoveFromCart()
        {
            foreach(var i in MyItemsToPay.ItemToPay)
            {
                await CrossCloudFirestore.Current
                       .Instance
                       .Collection("Carts")
                       .Document(i.documentID)
                       .DeleteAsync();
            }
        }
        private async Task ConfirmPayment()
        {
            IsLoading = true;
            try
            {
                if (!string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(PhoneNo))
                {
                    PaymentInfo paymentInfo = new PaymentInfo()
                    {
                        PhoneNo = PhoneNo,
                        Address = Address,
                        FullName = FullName,
                        Note = Notes,
                        MyItemsToPay = itemsToPay,
                        PaidAmount = TotalAmountValue,
                        PromoCode = PromoCode,
                        CreatedTime = DateTime.Now,
                        UserId = Preferences.Get("UserId", ""),
                        OrderStatus = "Processing",
                    };
                    var i = CrossCloudFirestore.Current
                                             .Instance
                                             .Collection("Payment")
                                             .AddAsync(paymentInfo)
                                             .ContinueWith(t =>
                                             {
                                                 System.Diagnostics.Debug.WriteLine(t.Exception);
                                             }, TaskContinuationOptions.OnlyOnFaulted);
                    if(i.Status == TaskStatus.WaitingForActivation)
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Your Order has been sent successfully", "Ok");
                        await SendEmail();
                        await RemoveFromCart();
                        Application.Current.MainPage = new CustomNavigationPage(new MainPage());
                    }
                    IsLoading = false;
                }
                else
                {
                    IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("", "Please Enter Required fields", "Ok");
                }
            }
            catch (Exception ex)
            {
                IsLoading = false;

            }

        }
    }
    public class MyOrders
    {
        public string status { get; set; }
        public SavedItems myItems { get; set; }
    }
}