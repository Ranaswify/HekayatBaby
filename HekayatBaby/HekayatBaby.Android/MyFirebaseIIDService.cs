//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using Firebase.Iid;
//using Firebase.Messaging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace HekayatBaby.Droid
//{
//    [Service]
//    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
//    public class MyFirebaseIIDService : FirebaseInstanceIdService
//    {
//        const string TAG = "MyFirebaseIIDService";
//        public override void OnTokenRefresh()
//        {
//            var refreshedToken = FirebaseInstanceId.Instance.Token;
//            System.Diagnostics.Debug.WriteLine($"======================token:  {refreshedToken} =====================");
//            //SendRegistrationToServer(refreshedToken);
//        }
//        void SendRegistrationToServer(string token)
//        {
//            // Add custom implementation, as needed.
//        }
//    }
//}