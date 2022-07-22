using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Plugin.PushNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HekayatBaby.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer) : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            var options = new FirebaseOptions.Builder()
 .SetApplicationId("1:563170435318:android:f1f2e2409690379cc98cd7")
 .SetApiKey("AIzaSyDZiMNegcUYFUMntytdPqWMbFxZcOdtfAc")
 .SetProjectId("aboutthekids-788ac").Build();
            FirebaseApp.InitializeApp(this, options);


            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                PushNotificationManager.DefaultNotificationChannelId = "DefaultChannel";

                //Change for your default notification channel name here
                PushNotificationManager.DefaultNotificationChannelName = "General";
            }

            //If debug you should reset the token each time.
#if DEBUG
            PushNotificationManager.Initialize(this, true);
#else
              PushNotificationManager.Initialize(this,false);
#endif
            // var data = Intent.ExtraIntent;
            //Handle notification when app is closed here
            CrossPushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                
            };

            CrossPushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"Token : {p.Token}");
            };

            CrossPushNotification.Current.OnNotificationOpened += (s, p) =>
            {
            };
        }

    }
}