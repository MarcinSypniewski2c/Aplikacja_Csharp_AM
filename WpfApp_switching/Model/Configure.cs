using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WpfApp_switching.ViewModel;


namespace WpfApp_switching.Model
{
    public class Configure
    {
        static readonly string ipAddressDefault = "192.168.0.112";
        public string IpAddress;
        static readonly int sampleTimeDefault = 1000;
        public int SampleTime;
        public readonly int MaxSampleNumber = 100;
        public double XAxisMax
        {
            get
            {
                return MaxSampleNumber * SampleTime / 1000.0;
            }
            private set { }
        }

        public Configure()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\Marcin\Documents\Config_App_AM.txt"))
            {
                string json = r.ReadToEnd();
                List<data> config_data = JsonConvert.DeserializeObject<List<data>>(json);
                IpAddress = config_data[0].Ip_data;
                SampleTime = config_data[0].SampleTime_data;

            }
        }

        public Configure(string ip, int st)
        {
            IpAddress = ip;
            SampleTime = st;
        }
    }
}
