using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace laboratory.common
{
    public class ButtonAction : INotifyPropertyChanged
    {
        public ImageSource ImageSource { get; set; }

        private string _name;
        public string NameAction 
        { 
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NameAction"));
            }
        }

        public Action Action { get; set; }

        private Visibility _visibility;
        public Visibility NameActionVisibly
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NameActionVisibly"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        
        public ButtonAction(string name, string image, Action action)
        {
            NameAction = name;
            ImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath($"../../Resources/icons/{image}")));
            NameActionVisibly = Visibility.Collapsed;
            Action = action;
        }
    }
}
