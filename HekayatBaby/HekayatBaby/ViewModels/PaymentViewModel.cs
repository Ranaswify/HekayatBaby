using HekayatBaby.Controls;
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


namespace HekayatBaby.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {

        private ObservableCollection<SavedItems> allSaved;
        private ItemsToPay itemsToPay;

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

        public PaymentViewModel()
        {

        }

        void SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
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
                    "User Info: " + "\n"+
                    "Name: " + FullName + "\n" + "Address: " + Address + "\n" + "Phone number: " + PhoneNo
                    +"\n" + "Order Info:" + "\n" + "Total Amount: " + MyItemsToPay.TotalAmount + "\n" + "Items: " + "\n" + list;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("AboutTheKids7@gmail.com", "AboutTheKids22");

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
                        PaidAmount = MyItemsToPay.TotalAmount,
                        PromoCode = PromoCode
                    };
                    var i = CrossCloudFirestore.Current
                                             .Instance
                                             .Collection("Payment")
                                             .AddAsync(paymentInfo)
                                             .ContinueWith(t =>
                                             {
                                                 System.Diagnostics.Debug.WriteLine(t.Exception);
                                             }, TaskContinuationOptions.OnlyOnFaulted);
                    if(i.Status== TaskStatus.WaitingForActivation)
                    {
                        SendEmail();
                        await RemoveFromCart();
                        await Application.Current.MainPage.DisplayAlert("", "Your has been sent successfully", "Ok");
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
}
