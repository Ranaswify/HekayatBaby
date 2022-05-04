using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using HekayatBaby.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace HekayatBaby.Helper
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://aboutthekids-788ac-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("");
        public async Task<List<Users>> GetAllPersons()
        {
            return (await firebase
              .Child("Users")
              .OnceAsync<Users>()).Select(item => new Users
              {
                  Username = item.Object.Username,
                  PhoneNo = item.Object.PhoneNo,
                  UserId = item.Object.UserId,
                  Password = item.Object.Password
              }).ToList();
        }

        public async Task<ObservableCollection<PromoCode>> GetAllPromo()
        {
            try
            {
                var group = await CrossCloudFirestore.Current
                                    .Instance
                                    .CollectionGroup("PromoCode")
                                    .GetAsync();
                var yourModels = group.ToObjects<PromoCode>().ToList();

                return new ObservableCollection<PromoCode>(yourModels);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<PromoCode>();
            }
        }

        public async Task<FirebaseObject<Users>> AddPerson(string name, string phoneNo, string password)
        {
            try
            {
               var response = await firebase.Child("Users").PostAsync(
              new Users() { Username = name, PhoneNo = phoneNo, Password = password, UserId = "US" + phoneNo + name });
                return response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> GetPerson(string username, string phoneNo)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Users")
              .OnceAsync<Users>();
            return allPersons.Where(a => a.Password == phoneNo && a.Username==username).FirstOrDefault();
        }


        public async Task<ObservableCollection<Items>> GetAllItems(string dbName)
        {
            var group = await CrossCloudFirestore.Current
                                     .Instance
                                     .CollectionGroup(dbName)
                                     .GetAsync();
            var yourModels = group.ToObjects<Items>().ToList();
            return new ObservableCollection<Items>(yourModels);
        }

     
        public async Task<ObservableCollection<SavedItems>> GetAllSavedItems()
        {
            try
            {
                var group = await CrossCloudFirestore.Current
                                    .Instance
                                    .CollectionGroup("Carts")
                                    .GetAsync();
                var yourModels = group.ToObjects<SavedItems>().ToList();
                var m = group.Documents.ToList();
               
                for(int i = 0; i < m.Count; i++)
                {
                    yourModels[i].documentID = m[i].Id;
                }
              
                return new ObservableCollection<SavedItems>(yourModels);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<SavedItems>();
            }
           
        }

        public async Task<ObservableCollection<PaymentInfo>> GetMyOrders()
        {
            try
            {
                var group = await CrossCloudFirestore.Current
                                    .Instance
                                    .CollectionGroup("Payment")
                                    .GetAsync();
                var yourModels = group.ToObjects<PaymentInfo>().ToList();
                var m = group.Documents.ToList();

                for (int i = 0; i < m.Count; i++)
                {
                    yourModels[i].documentID = m[i].Id;
                }

                return new ObservableCollection<PaymentInfo>(yourModels);
            }
            catch (Exception ex)
            {
                return new ObservableCollection<PaymentInfo>();
            }

        }

        public async Task UpdatePerson(PaymentInfo paymentInfo, string yourdocument)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection("Payment")
                         .Document(yourdocument)
                         .UpdateAsync(paymentInfo);
        }


    }
}