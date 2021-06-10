using System;
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using WpfApp_switching.Model;

namespace WpfApp_switching.ViewModel
{

    public class Measurements
    {
        public double temperature { get; set; }
        public double pressure { get; set; }
    }

    class GraphViewModel : BaseViewModel
    {
        public PlotModel Plot1 { get; set; }
        public PlotModel Plot2 { get; set; }

        public ButtonCommand StartBtn { get; set; }
        public ButtonCommand StopBtn { get; set; }

        public string Temp { get; private set; }
        public string Pres { get; private set; }

        public double i = 0;

        System.Windows.Threading.DispatcherTimer disTim;
       

        private void UpdatePlot(double t, double d, PlotModel Plot_n)
        {
            LineSeries lineSeries = Plot_n.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                Plot_n.Axes[0].Minimum = (t - config.XAxisMax);
                Plot_n.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            Plot_n.InvalidatePlot(true);
        }

        public void update_data()
        {
            IoTserver update_server = new IoTserver(ipAddress);
            var measurements = update_server._download_serialized_json_data<Measurements>();
            this.Temp = measurements.temperature.ToString();
            this.Pres = measurements.pressure.ToString();
            double temp_plot = measurements.temperature;
            UpdatePlot(i, measurements.temperature, Plot1);
            UpdatePlot(i, measurements.pressure, Plot2);
            OnPropertyChanged("Temp");
            OnPropertyChanged("Pres");
            i += config.SampleTime / 1000.0;
        }

        public void disTim_tick(object sender, EventArgs e)
        {
            update_data();
        }

        public void StartTimer()
        {
            if (disTim == null)
            {
                disTim = new System.Windows.Threading.DispatcherTimer();
                disTim.Tick += disTim_tick;
                //disTim.Interval = new TimeSpan(0, 0, 1);
                double std = Convert.ToDouble(config.SampleTime);
                TimeSpan st_ms = TimeSpan.FromMilliseconds(std);
                disTim.Interval = st_ms;
                disTim.Start();
            }
        }

        public void StopTimer()
        {
            disTim.Stop();
            disTim = null;
        }


        public GraphViewModel()
        {

            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;

            StartBtn = new ButtonCommand(StartTimer);
            StopBtn = new ButtonCommand(StopTimer);

            Plot1 = new PlotModel { Title = "Temperature" };

            Plot1.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Plot1.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = -1,
                Maximum = 11,
                Key = "Vertical",
                Unit = "C",
                Title = "Temp"
            });
            Plot1.Series.Add(new LineSeries() { Title = "temperature", Color = OxyColor.Parse("#FFFF0000") });

            Plot2 = new PlotModel { Title = "Pressure" };

            Plot2.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Plot2.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 975,
                Maximum = 1050,
                Key = "Vertical",
                Unit = "hPa",
                Title = "Pres"
            });
            Plot2.Series.Add(new LineSeries() { Title = "pressure", Color = OxyColor.Parse("#FF0000FF") });
        }

    }
}
