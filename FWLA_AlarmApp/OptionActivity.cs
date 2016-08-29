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
    [Activity(Label = "Options")]
    public class OptionActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Option);

            Button btncleardb = FindViewById<Button>(Resource.Id.btnClearDatabase);

            btncleardb.Click += delegate
            {
                DBProvider.ClearAlarms();
            };
            // Create your application here
        }
    }
}