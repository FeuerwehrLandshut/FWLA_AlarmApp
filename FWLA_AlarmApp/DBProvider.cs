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
    public static class DBProvider
    {
        public static string DBPath
        {
            get
            {
                return System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "fwla_alarms.db3");
            }
        }

        public static void InitDatabase()
        {
            SQLiteConnection db = new SQLiteConnection(DBPath);
            db.CreateTable<Alarm>();
            //db.Insert(new Alarm() { ID = 0, Name = "test1", Content = "nada", Confirmed = false, Time = DateTime.Now });
            //db.Insert(new Alarm() { ID = 1, Name = "test2", Content = "xyz", Confirmed = false, Time = DateTime.Now.AddDays(-3) });
        }

        public static List<Alarm> SelectAll()
        {
            SQLiteConnection db = new SQLiteConnection(DBPath);
            return db.Table<Alarm>().ToList<Alarm>();
        }

        public static Alarm SelectById(int _id)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath);
            return db.Get<Alarm>(_id);
        }

        public static void AddAlarm(Alarm a)
        {
            SQLiteConnection db = new SQLiteConnection(DBPath);
            db.Insert(a);
        }

        public static void ClearAlarms()
        {
            SQLiteConnection db = new SQLiteConnection(DBPath);
            db.DropTable<Alarm>();
            db.CreateTable<Alarm>();
        }
    }
}