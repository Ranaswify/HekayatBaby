//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Support.V4.App;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using Firebase.Messaging;
////using Firebase.Messaging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace HekayatBaby.Droid
//{
//    [Service]
//    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
//    public class MyFirebaseMessagingService : FirebaseMessagingService
//    {
//        const string TAG = "MyFirebaseMsgService";
//        public override void OnMessageReceived(RemoteMessage message)
//        {
//            Log.Debug(TAG, "From: " + message.From);
//            var body = message.GetNotification();
//            Log.Debug(TAG, "Notification Message Body: " + body);
//            SendNotification(body);
//        }

//        void SendNotification(string messageBody)
//        {
//            var intent = new Intent(this, typeof(MainActivity));
//            intent.AddFlags(ActivityFlags.ClearTop);


//            var pendingIntent = PendingIntent.GetActivity(this, 0, intent,
//                                                          PendingIntentFlags.OneShot);

//            var notificationBuilder = new NotificationCompat.Builder(this)
//                                      .SetSmallIcon(Resource.Drawable.notification_bg_normal)
//                                      .SetContentTitle("About The Kids")
//                                      .SetContentText(messageBody)
//                                      .SetAutoCancel(true)
//                                      .SetContentIntent(pendingIntent);

//            var notificationManager = NotificationManagerCompat.From(this);
//            notificationManager.Notify(0, notificationBuilder.Build());
//        }
//    }
//}