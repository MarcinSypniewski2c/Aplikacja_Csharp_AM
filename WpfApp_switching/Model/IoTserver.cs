using System;
using System.Net;
using Newtonsoft.Json;

namespace WpfApp_switching.Model
{
    public class IoTserver
    {
        public string ip;

        public IoTserver(string _ip)
        {
            ip = _ip;
        }

        public T _download_serialized_json_data<T>() where T : new()
        {
            using (var w = new WebClient())
            {
                string url = $"http://{ip}/temp_am6.php";
                var json_data = string.Empty;
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        public void _upload_serialized_json_data(string json_post)
        {
            using (var w = new WebClient())
            {
                string url = $"http://{ip}/led_mock.php";
                try
                {
                    w.UploadString(url,json_post);
                }
                catch (Exception) { }
            }
        }

    }

}
