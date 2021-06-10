using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_switching.Model;
using Newtonsoft.Json;

namespace WpfApp_switching.ViewModel
{
    public class data
    {
        public string Port_data;
        public string Ip_data;
        public string API_data;
        public int SampleTime_data;
    }

    class ListViewModel : BaseViewModel
    {
        public ButtonCommand UPCBtn { get; set; }

        public void SaveConfig()
        {
            Server = new IoTserver(IpAddress);
            config = new Configure(ipAddress, sampleTime);
            string ip_param = $"{ipAddress}";
            int st_param = sampleTime;
            List<data> _data = new List<data>();
            _data.Add(new data()
            {
                Port_data="25565",
                Ip_data = ip_param,
                API_data = "8.10",
                SampleTime_data = st_param
            });

            string json = JsonConvert.SerializeObject(_data.ToArray());
            File.WriteAllText(@"C:\Users\Marcin\Documents\Config_App_AM.txt", json); ;
        }

        public ListViewModel()
        {
            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;
            UPCBtn = new ButtonCommand(SaveConfig);

        }

    }
}
