using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Gcm;
using Android.Util;
using Android.Net;
using Android.Graphics;

namespace Revelaciones_Marianas
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    public class MyGcmListenerService : GcmListenerService
    {
        public override void OnMessageReceived(string from, Bundle data)
        {
            var message = data.GetString("message");
            Log.Debug("MyGcmListenerService", "From:    " + from);
            Log.Debug("MyGcmListenerService", "Message: " + message);
            SendNotification(message);
        }

        void SendNotification(string message)
        {
            Intent intent = new Intent(Intent.ActionView, Uri.Parse("http://www.revelacionesmarianas.com/"));

            intent.AddFlags(ActivityFlags.ClearTop);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            Notification.Builder notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.notificationIcon)
                .SetContentTitle(GetString(Resource.String.app_name))
                .SetContentText(GetString(Resource.String.new_message))
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .SetVibrate(new long[] { 1000, 1000, 1000, 1000, 1000 })
                .SetLights(Color.White.ToArgb(), 3000, 3000);

            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}