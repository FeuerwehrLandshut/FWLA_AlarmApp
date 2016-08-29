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
using Java.Lang;

namespace FWLA_AlarmApp
{
    public class AlarmAdapter : BaseAdapter<Alarm>
    {
        Alarm[] alarms;
        Activity context;

        public AlarmAdapter(Activity context, Alarm[] alarms) : base()
        {
            this.context = context;
            this.alarms = alarms;
        }
        public override long GetItemId(int position)
        {
            return alarms[position].ID;
        }

        public override Alarm this[int pos]
        {
            get { return alarms[pos]; }
        }

        public override int Count
        {
            get { return alarms.Length; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = alarms[position].Name;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = alarms[position].Time.ToLongDateString() + " - " + alarms[position].Time.ToShortTimeString();
            //view.FindViewById<CheckBox>(Android.Resource.Id.Checkbox).Checked = alarms[position].Confirmed;
            return view;
        }

        //public override Java.Lang.Object GetItem(int position)
        //{
        //    throw new NotImplementedException();
        //}
    }
}