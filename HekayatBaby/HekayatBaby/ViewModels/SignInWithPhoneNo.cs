using Plugin.FirebaseAuth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HekayatBaby.ViewModels
{
    public class SignInWithPhoneNo
    {
        public string PhoneNumber { get; set; }
        string VerificationCode { get; set; }
        public ICommand OnLoginCommand { get; set; }

        public SignInWithPhoneNo()
        {
            OnLoginCommand = new Command(async () => await SignIn());

        }

        public async Task SignIn()
        {

            try
            {
                PhoneNumberVerificationResult verificationResult;

                verificationResult = await CrossFirebaseAuth.Current.PhoneAuthProvider
                          .VerifyPhoneNumberAsync(PhoneNumber);

                if (verificationResult.Credential != null)
                {
                   // await NavigationService.GoBackAsync(verificationResult.Credential);
                }
                else
                {
                   // var verificationCode = 

                    //if (verificationCode != null)
                    //{
                    //    var credential = CrossFirebaseAuth.Current.PhoneAuthProvider
                    //        .GetCredential(verificationResult.VerificationId, verificationCode);

                    //    await Task.Delay(TimeSpan.FromMilliseconds(1));

                    //   // await NavigationService.GoBackAsync(credential);
                    //}
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);

              //  await _pageDialogService.DisplayAlertAsync("Failure", e.Message, "OK");
            }
        }
    }
}
