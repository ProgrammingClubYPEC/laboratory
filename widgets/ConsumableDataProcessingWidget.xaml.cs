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
    /// Логика взаимодействия для ConsumableDataProcessingWidget.xaml
    /// </summary>
    public partial class ConsumableDataProcessingWidget : Page, IWidget
    {
        public IPage ParentPage { get; set; }
        public IConfigWidget CurrentConfigWidget { get; set; }
        public List<IConfigWidget> ConfigWidgets { get; set; }

        public ConsumableDataProcessingWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;
        }

        public void UpdateData()
        {
            throw new NotImplementedException();
        }

        public void ChangeConfigWidget<T>()
        {
            throw new NotImplementedException();
        }
    }
}
