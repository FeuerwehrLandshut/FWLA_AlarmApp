using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Gms.Common;
using Android.Util;
using System.Collections.Generic;

namespace FWLA_AlarmApp
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        static readonly List<IParcelable> alarms = new List<IParcelable>();

        TextView msgText;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnShowAlarms = FindViewById<Button>(Resource.Id.btnShowAlarms);

            btnShowAlarms.Click += delegate
            {
                var intent = new Intent(this, typeof(ListAlarmsActivity));
                StartActivity(intent);
            };

            Button btnOpenSettings = FindViewById<Button>(Resource.Id.btnOpenSettings);

            btnOpenSettings.Click += delegate
            {
                var intent = new Intent(this, typeof(OptionActivity));
                StartActivity(intent);
            };

            msgText = FindViewById<TextView>(Resource.Id.msgText);

            DBProvider.InitDatabase();

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
                    msgText.Text = "Sorry, this device is not supported...";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }
    }
}

