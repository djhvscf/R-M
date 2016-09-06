using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Android.Content.PM;

namespace Revelaciones_Marianas
{
    [Activity(Label = "Revelaciones Marianas", MainLauncher = true, Icon = "@drawable/icon", 
        ConfigurationChanges = ConfigChanges.Locale | ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        TextView msgText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            msgText = FindViewById<TextView>(Resource.Id.msgText);

            if (IsPlayServicesAvailable())
            {
                var intent = new Intent(this, typeof(RegistrationIntentService));
                StartService(intent);
            }
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                else
                {
                    msgText.Text = GetString(Resource.String.error);
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = GetString(Resource.String.thanks_install);
                return true;
            }
        }
    }
}

