using laboratory.interfaces;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace laboratory
{
    /// <summary>
    /// Логика взаимодействия для CommonWindow.xaml
    /// </summary>
    public partial class CommonWindow : Window
    {
        public CommonWindow(IPage page)
        {
            InitializeComponent();
            page.ParentWindow = this;
            ContentFrame.Content = page;
            Width = (page as Page).Width;
            Height = (page as Page).Height;
        }
    }
}
