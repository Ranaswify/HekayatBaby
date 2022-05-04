using HekayatBaby.Models;
using HekayatBaby.Views;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                OnPropertyChanged();
            }
        }
        public ICommand OnLoginWithFacebookCommand { get; set; }

        IFacebookClient _facebookService = CrossFacebookClient.Current;
        public BaseViewModel()
        {
            OnLoginWithFacebookCommand = new Command(async () => await LoginFacebookAsync());
        }

        async Task LoginFacebookAsync()
        {
            IsLoading = true;
            try
            {
                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }
                var loginData = await CrossFacebookClient.Current.LoginAsync(new string[] { "email" });
                if (loginData.Status == FacebookActionStatus.Completed)
                {
                    var jsonData = await CrossFacebookClient.Current.RequestUserDataAsync(new string[] { "id", "email", "first_name", "last_name", "picture" }, new string[] { "email" });
                    if (jsonData.Status == FacebookActionStatus.Completed)
                    {
                        // Debug.WriteLine(jsonData.Data.ToString());
                        var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(jsonData.Data));

                        Preferences.Set("UserId", facebookProfile.UserId);
                        Preferences.Set("UserName", facebookProfile.FirstName+" "+facebookProfile.LastName);
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());

                        IsLoading = false;
                    }
                }
                else
                {
                    //Debug.WriteLine(loginData.Message);
                    IsLoading = false;
                }

            }
            catch (Exception ex)
            {
                // Debug.WriteLine(ex.ToString());
                IsLoading = false;
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}