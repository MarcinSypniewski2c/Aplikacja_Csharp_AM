using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }

        public void _upload_serialized_json_data(string json_post)
        {
            using (var w = new WebClient())
            {
                string url = $"http://{ip}/led_mock.php";
                // attempt to download JSON data as a string
                try
                {
                    w.UploadString(url,json_post);
                }
                catch (Exception) { }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                //return !string.IsNullOrEmpty(json_post) ? JsonConvert.DeserializeObject<T>(json_post) : new T();
            }
        }

    }

}
