using HekayatBaby.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public string username { get; set; }
        public string password { get; set; }
        public string phoneNo { get; set; }

        public ICommand OnSignUpCommand { get; set; }

        public SignUpViewModel()
        {
            OnSignUpCommand = new Command(async () => await OnSignUp());
        }

        async Task OnSignUp()
        {

            IsLoading = true;

            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(phoneNo))
                {
                    await firebaseHelper.AddPerson(username, phoneNo, password);
                    await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                    IsLoading = false;
                }
                else
                {
                    if (string.IsNullOrEmpty(username))
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Please enter Username", "Ok");
                        IsLoading = false;
                    }
                    else if (string.IsNullOrEmpty(password))
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Please enter Password", "Ok");
                        IsLoading = false;
                    }
                    else if (string.IsNullOrEmpty(phoneNo))
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Please enter Phone number", "Ok");
                        IsLoading = false;
                    }
                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
            }

        }

    }
}