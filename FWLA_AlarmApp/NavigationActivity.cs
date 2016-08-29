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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Globalization;

namespace FWLA_AlarmApp
{
    [Activity(Label = "NavigationActivity")]
    public class NavigationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            long id = Intent.GetLongExtra("alarm", 0);
            Alarm a = DBProvider.SelectById((int)id);
            SetContentView(Resource.Layout.Navigation);
            
            string[] locs = a.Location.Split(',');
            double lat = double.Parse(locs[0], new CultureInfo("en-US"));
            double lon = double.Parse(locs[1], new CultureInfo("en-US"));

            LatLng location = new LatLng(lat, lon);
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            //builder.Bearing(155);
            //builder.Tilt(65);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            GoogleMap map = mapFrag.Map;
            if (map != null)
            {
                map.MapType = GoogleMap.MapTypeNormal;
                map.UiSettings.ZoomControlsEnabled = true;
                map.UiSettings.CompassEnabled = true;
                map.MoveCamera(cameraUpdate);

                MarkerOptions markerOpt1 = new MarkerOptions();
                markerOpt1.SetPosition(location);
                markerOpt1.SetTitle(a.Name);
                map.AddMarker(markerOpt1);
            }

        }
    }
}