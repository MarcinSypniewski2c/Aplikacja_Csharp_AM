using System;
using System.Windows.Media;

namespace WpfApp_switching.ViewModel
{
    public class led_matrix_data
    {
        public int row;
        public int col;
        public int red;
        public int green;
        public int blue;
    }

    public class LedViewModel : BaseViewModel
    {
        private Random rand = new Random();

        public ButtonCommand SendBtn { get; set; }
        public ButtonCommand ClearBtn { get; set; }

        private Action<String, Color> setColorHandler;
        private Action setSendHandler;
        private Action setClearHandler;

        public ButtonCommandWithParameters CommonButtonCommand { get; set; }

        private byte _r;
        public int R
        {
            get
            {
                return _r;
            }
            set
            {
                if (_r != (byte)value)
                {
                    _r = (byte)value;
                    SetPreviewColor(_r, _g, _b);
                    OnPropertyChanged("R");
                }
            }
        }

        private byte _g;
        public int G
        {
            get
            {
                return _g;
            }
            set
            {
                if (_g != (byte)value)
                {
                    _g = (byte)value;
                    SetPreviewColor(_r, _g, _b);
                    OnPropertyChanged("G");
                }
            }
        }

        private byte _b;
        public int B
        {
            get
            {
                return _b;
            }
            set
            {
                if (_b != (byte)value)
                {
                    _b = (byte)value;
                    SetPreviewColor(_r, _g, _b);
                    OnPropertyChanged("B");
                }
            }
        }

        private SolidColorBrush _selectedColor;
        public SolidColorBrush SelectedColor
        {
            get
            {
                return _selectedColor;
            }
            set
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged("SelectedColor");
                }
            }
        }
        public LedViewModel()
        {
            ipAddress = config.IpAddress;

            CommonButtonCommand = new ButtonCommandWithParameters(SetButtonColor);
            SendBtn = new ButtonCommand(SendLed);
            ClearBtn = new ButtonCommand(ClearLed);
        }

        public void SetHLed(Action<String, Color> handler)
        {
            setColorHandler = handler;
        }

        public void SetSLed(Action handler)
        {
            setSendHandler = handler;
        }

        public void SetCLed(Action handler)
        {
            setClearHandler = handler;
        }

        public string LedIndexToTag(int i, int j)
        {
            return "BM" + i.ToString() + j.ToString();
        }

        private void SetPreviewColor(byte r, byte g, byte b)
        {
            byte a = (byte)((r + b + g) / 3);
            SelectedColor = new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        private void SetButtonColor(string parameter)
        {
            setColorHandler(parameter, SelectedColor.Color);
        }

        private void SendLed()
        {
            setSendHandler();
        }

        public void ClearLed()
        {
            setClearHandler();
        }

    }
}
