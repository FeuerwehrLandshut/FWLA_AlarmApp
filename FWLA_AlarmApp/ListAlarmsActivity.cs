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
    [Activity(Label = "@string/ListAlarmsActivity")]
    public class ListAlarmsActivity : ListActivity
    {
        Alarm[] alarms;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            alarms = DBProvider.SelectAll().ToArray();
            ListAdapter = new AlarmAdapter(this, alarms);
            ListView.FastScrollEnabled = true;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var intent = new Intent(this, typeof(ViewAlarmActivity));
            intent.PutExtra("alarm", id);
            StartActivity(intent);
            
        }
    }
}