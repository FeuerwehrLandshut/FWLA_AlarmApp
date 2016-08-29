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
using SQLite;

namespace FWLA_AlarmApp
{
    [Table("Alarms")]
    public class Alarm //: IParcelable
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public bool Confirmed { get; set; }


        public Alarm()
        {
            ID = 0;
            Name = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Time = DateTime.Now;
            Confirmed = false;
        }

        public Alarm(string name, string content, string location, DateTime time, bool confirmed = false)
        {
            this.Name = name;
            this.Content = content;
            this.Location = location;
            this.Time = time;
            this.Confirmed = confirmed;
        }

        //public IntPtr Handle
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public int DescribeContents()
        //{
        //    return 0;
        //}

        //public void Dispose()
        //{
        //    Name = null;
        //    Content = null;
        //}

        //public void WriteToParcel(Parcel dest, [GeneratedEnum] ParcelableWriteFlags flags)
        //{
        //    dest.WriteInt(ID);
        //    dest.WriteString(Name);
        //    dest.WriteString(Content);
        //    dest.WriteLong(Time.Ticks);
        //}

        //public Alarm(Parcel incom)
        //{
        //    ID = incom.ReadInt();
        //    Name = incom.ReadString();
        //    Content = incom.ReadString();
        //    Time = new DateTime(incom.ReadLong());
        //}
    }
}