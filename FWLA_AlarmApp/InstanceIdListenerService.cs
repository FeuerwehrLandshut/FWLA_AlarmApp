using Android.App;
using Android.Content;
using Android.Gms.Gcm.Iid;

namespace FWLA_AlarmApp
{
    [Service(Exported = false), IntentFilter(new[] { "com.google.android.gms.iid.InstanceID" })]
    class FWLAInstanceIDListenerService : InstanceIDListenerService
    {
        public override void OnTokenRefresh()
        {
            var intent = new Intent(this, typeof(RegistrationIntentService));
            StartService(intent);
        }
    }
}