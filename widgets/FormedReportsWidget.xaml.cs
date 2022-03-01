using laboratory.database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laboratory.widgets
{
    /// <summary>
    /// Логика взаимодействия для FormedReportsWidget.xaml
    /// </summary>
    public partial class FormedReportsWidget : Page, IWidget
    {
        public IPage ParentPage { get; set; }
        public IWidget ParentWidget { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IWidget CurrentWidget { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IWidget> Widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FormedReportsWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;
            UpdateData();
        }
        
        public void ChangeConfigWidget<T>()
        {
            throw new NotImplementedException();
        }

        public void UpdateData()
        {
        }
    }
}
