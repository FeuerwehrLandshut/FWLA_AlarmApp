using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MessageSender
{
    class MessageSender
    {
        public const string API_KEY = "AIzaSyCYx5T0YUWEAjR5bo0hb0xHh33zCs5POkk";
        public const string SENDER_ID = "213716459500";

        static void Main(string[] args)
        {
            var jGcmData = new JObject();
            var jData = new JObject();
            jData = JObject.FromObject(new Data() { location = "48.5502816,12.1324568", message = "here should be more text", title = "FWLA LZ7" });

            Console.WriteLine(jData.ToString());

            //string to = Console.ReadLine();
            //jGcmData.Add("to", to);
            jGcmData.Add("to", "/topics/global");
            jGcmData.Add("priority", "high");
            jGcmData.Add("data", jData);

            var url = new Uri("https://gcm-http.googleapis.com/gcm/send");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Authorization", "key=" + API_KEY);
                    client.DefaultRequestHeaders.TryAddWithoutValidation(
                        "Sender", "id=" + SENDER_ID);

                    Task.WaitAll(client.PostAsync(url,
                        new StringContent(jGcmData.ToString(), Encoding.Default, "application/json"))
                            .ContinueWith(response =>
                            {
                                var x = response.Result.Content.ReadAsStringAsync().ContinueWith(y =>
                                {
                                    Console.WriteLine(y.Result.ToString());
                                    Console.WriteLine("Message sent: check the client device notification tray.");
                                });
                            }));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to send GCM message:");
                Console.Error.WriteLine(e.StackTrace);
            }
            Console.ReadKey();
        }
    }

    public class Data
    {
        public string location;
        public string message;
        public string title;
    }
}