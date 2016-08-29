using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Gcm;
using Android.Util;
using System;

namespace FWLA_AlarmApp
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    public class FWLAGcmListenerService : GcmListenerService
    {
        public override void OnMessageReceived(string from, Bundle data)
        {
            var title = data.GetString("title");
            var message = data.GetString("message");
            var location = data.GetString("location");

            var a = new Alarm(title, message, location, DateTime.Now);

            DBProvider.AddAlarm(a);

            Log.Debug("FWLAGcmListenerService", "From:    " + from);
            Log.Debug("FWLAGcmListenerService", "Message: " + message);
            SendNotification(a);
        }

        void SendNotification(Alarm a)
        {
            var intent = new Intent(this, typeof(ViewAlarmActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            intent.PutExtra("alarm", (long)a.ID);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var wavUri = Android.Net.Uri.Parse("android.resource://" + this.PackageName + "/raw/" + Resource.Raw.alarm1);

            var notificationBuilder = new Notification.Builder(this)
                .SetCategory(Notification.CategoryAlarm)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(a.Name)
                .SetContentText(a.Content)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .SetLights(System.Drawing.Color.Blue.ToArgb(), 1000, 500)
                .SetPriority(2)
                .SetVibrate(new long[] { 750, 1000, 750, 1000, 750, 1000, 750, 1000 })
#if !DEBUG
                .SetSound(wavUri);
#else
                ;
#endif
            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(a.ID, notificationBuilder.Build());
        }
    }
}