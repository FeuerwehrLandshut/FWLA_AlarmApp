using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FWLA_AlarmApp
{
    [Activity(Label = "ViewAlarmActivity")]
    public class ViewAlarmActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            long id = Intent.GetLongExtra("alarm", 0);
            Alarm a = DBProvider.SelectById((int)id); // i know, but i don't want...
            SetContentView(Resource.Layout.ViewAlarm);

            TextView tvTitle = FindViewById<TextView>(Resource.Id.tvTitle);
            TextView tvTime = FindViewById<TextView>(Resource.Id.tvTime);
            TextView tvContent = FindViewById<TextView>(Resource.Id.tvContent);
            Button btnNavigation = FindViewById<Button>(Resource.Id.btnNavigation);

            tvTitle.Text = a.Name;
            tvTime.Text = a.Time.ToShortTimeString() + " - " + a.Time.ToLongDateString();
            tvContent.Text = a.Content;

            btnNavigation.Click += delegate
            {
                //var geoUri = Android.Net.Uri.Parse("geo:" + a.Location);
                //var mapIntent = new Intent(Intent.ActionView, geoUri);
                //StartActivity(mapIntent);
                //return;

                var intent = new Intent(this, typeof(NavigationActivity));
                intent.PutExtra("alarm", id);
                StartActivity(intent);
            };
        }
    }
}