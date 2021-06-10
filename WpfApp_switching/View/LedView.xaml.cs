using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp_switching.ViewModel;
using WpfApp_switching.Model;

namespace WpfApp_switching.View
{
    /// <summary>
    /// Interaction logic for LedView.xaml
    /// </summary>
    public partial class LedView : UserControl
    {
        private readonly int ledMatrixWidth = 8;
        private readonly int ledMatrixHeigth = 8;

        private readonly ViewModel.LedViewModel vm;

        public LedView()
        {
            InitializeComponent();

            vm = new LedViewModel();
            vm.SetHLed(SetButtonColor);
            vm.SetSLed(SendLed);
            vm.SetCLed(ClearLed);
            DataContext = vm;

            for (int i = 0; i < ledMatrixWidth; i++)
            {
                ButtonMatrixGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ButtonMatrixGrid.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
            }
            for (int i = 0; i < ledMatrixHeigth; i++)
            {
                ButtonMatrixGrid.RowDefinitions.Add(new RowDefinition());
                ButtonMatrixGrid.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
            }
            for (int i = 0; i < ledMatrixWidth; i++)
            {
                for (int j = 0; j < ledMatrixHeigth; j++)
                {
                    // <Button
                    Button led = new Button()
                    {
                        // Name = "BMij"
                        Name = vm.LedIndexToTag(i, j),
                        // CommandParameter = "BMij"
                        CommandParameter = vm.LedIndexToTag(i, j),
                        // Style="{StaticResource LedButtonStyle}"
                        Style = (Style)FindResource("LedButtonStyle"),
                        //Background="LightGray"
                        Background = new SolidColorBrush(Colors.LightGray)
                    };
                    // Command="{Binding CommonButtonCommand}" 
                    led.SetBinding(Button.CommandProperty, new Binding("CommonButtonCommand"));
                    // Grid.Column="i" 
                    Grid.SetColumn(led, i);
                    // Grid.Row="j"
                    Grid.SetRow(led, j);
                    // />

                    ButtonMatrixGrid.Children.Add(led);
                    ButtonMatrixGrid.RegisterName(led.Name, led);
                }
            }
        }
        private void SetButtonColor(String name, Color color)
        {
            (ButtonMatrixGrid.FindName(name) as Button).Background = new SolidColorBrush(color);
        }

        private void SendLed()
        {
            Color led_off = Colors.LightGray;
            List<led_matrix_data> led_on = new List<led_matrix_data>();
            for (int i = 0; i < ledMatrixWidth; i++)
            {
                for (int j = 0; j < ledMatrixHeigth; j++)
                {
                    String temp_btn_name = vm.LedIndexToTag(i, j);
                    Color temp_btn_color = ((SolidColorBrush)(ButtonMatrixGrid.FindName(temp_btn_name) as Button).Background).Color;
                    if (temp_btn_color != led_off)
                    {
                        led_on.Add(new led_matrix_data()
                        {
                            row = j,
                            col = i,
                            red = temp_btn_color.R,
                            green = temp_btn_color.G,
                            blue = temp_btn_color.B
                        });
                    }
                }
            }
            string json = JsonConvert.SerializeObject(led_on.ToArray());
            Configure led_config = new Configure();
            IoTserver led_server = new IoTserver(led_config.IpAddress);
            led_server._upload_serialized_json_data(json);
            
            File.WriteAllText(@"C:\Users\Marcin\Documents\LED_App_AM.txt", json); ;
        }

        private void ClearLed()
        {
            Color led_off = Colors.LightGray;
            List<led_matrix_data> led_on = new List<led_matrix_data>();
            for (int i = 0; i < ledMatrixWidth; i++)
            {
                for (int j = 0; j < ledMatrixHeigth; j++)
                {
                    String temp_btn_name = vm.LedIndexToTag(i, j);
                    Color temp_btn_color = ((SolidColorBrush)(ButtonMatrixGrid.FindName(temp_btn_name) as Button).Background).Color;
                    if (temp_btn_color != led_off)
                    {
                        (ButtonMatrixGrid.FindName(temp_btn_name) as Button).Background = new SolidColorBrush(led_off);
                        led_on.Add(new led_matrix_data()
                        {
                            row = j,
                            col = i,
                            red = led_off.R,
                            green = led_off.G,
                            blue = led_off.B
                        });
                    }
                }
            }
            string json = JsonConvert.SerializeObject(led_on.ToArray());
            Configure led_config = new Configure();
            IoTserver led_server = new IoTserver(led_config.IpAddress);
            led_server._upload_serialized_json_data(json);

            File.WriteAllText(@"C:\Users\Marcin\Documents\LED_App_AM.txt", json); ;
        }

    }
}
