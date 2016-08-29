using System;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;

namespace FWLA_AlarmApp
{
    [Service(Exported = false)]
    class RegistrationIntentService : IntentService
    {
        static object locker = new object();
        
        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                lock (locker)
                {
                    InstanceID instanceID = InstanceID.GetInstance(this);
                    string token = instanceID.GetToken(
                       "213716459500", GoogleCloudMessaging.InstanceIdScope);
#if DEBUG
                    instanceID.DeleteToken(token, GoogleCloudMessaging.InstanceIdScope);
                    instanceID.DeleteInstanceID();
#endif
                    token = instanceID.GetToken(
                       "213716459500", GoogleCloudMessaging.InstanceIdScope);
                    Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                    SendRegistrationToAppServer(token);
                    Subscribe(token);
                }
            }
            catch (Exception)
            {
                Log.Debug("RegistrationIntentService", "Failed to get a registration token");
                return;
            }
        }

        void SendRegistrationToAppServer(string token)
        { 
           //later
            // Add custom implementation here as needed.
        }

        void Subscribe(string token)
        {
            var pubSub = GcmPubSub.GetInstance(this);
            pubSub.Subscribe(token, "/topics/global", null);
        }
    }
}