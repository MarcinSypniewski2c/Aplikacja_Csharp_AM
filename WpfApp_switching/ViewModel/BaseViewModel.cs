using System;
using System.ComponentModel;
using WpfApp_switching.Model;

namespace WpfApp_switching.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public IoTserver Server;
        public Configure config = new Configure();

        public int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }

        public string ipAddress;
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
