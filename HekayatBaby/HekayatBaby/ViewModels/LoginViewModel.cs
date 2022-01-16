using HekayatBaby.Helper;
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
    public class LoginViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        string passwordEntryText;
        public string PasswordEntryText

        {
            set
            {
                if (passwordEntryText == value)
                    return;
                passwordEntryText = value;
                OnPropertyChanged();
            }
            get { return passwordEntryText; }
        }


        string userNameText;
        public string UserNameText

        {
            set
            {
                if (userNameText == value)
                    return;
                userNameText = value;
                OnPropertyChanged();
            }
            get { return userNameText; }
        }

        string showPasswordIcon= "&#xf06e;";
        public string ShowPasswordIcon

        {
            set
            {
                if (showPasswordIcon == value)
                    return;
                showPasswordIcon = value;
                OnPropertyChanged();
            }
            get { return showPasswordIcon; }
        }

        bool isShowPassword = true;
        public bool IsShowPassword

        {
            set
            {
                if (isShowPassword == value)
                    return;
                isShowPassword = value;
                OnPropertyChanged();
            }
            get { return isShowPassword; }
        }

        public LoginViewModel()
        {
            OnLoginCommand = new Command(async () => await OnLogin());
            ShowPasswordCommand = new Command(ShowPassword);
        }

        private void ShowPassword()
        {
            IsShowPassword =! IsShowPassword;
           
        }

        public ICommand OnLoginCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }

        async Task OnLogin()
        {
            // await Application.Current.MainPage.Navigation.PushAsync(new MainPage());

            IsLoading = true;
            try
            {
                if (!string.IsNullOrEmpty(UserNameText) && !string.IsNullOrEmpty(PasswordEntryText))
                {

                    var i = await firebaseHelper.GetPerson(UserNameText, PasswordEntryText);
                    if (i != null)
                    {
                        Preferences.Set("UserId", i.UserId);
                        Preferences.Set("UserName", i.Username);
                        await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                        IsLoading = false;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Wrong username or password", "Ok");
                        IsLoading = false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "Please enter required fields", "Ok");
                    IsLoading = false;
                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
                await Application.Current.MainPage.DisplayAlert("", "Please try again later", "Ok");

            }
        }

    }
}
