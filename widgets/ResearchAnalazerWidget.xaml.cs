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
    /// Логика взаимодействия для ResearchAnalazerWidget.xaml
    /// </summary>
    public partial class ResearchAnalazerWidget : Page, IWidget
    {
        public ResearchAnalazerWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;
            UpdateData();
        }

        public IPage ParentPage { get; set; }
        public IWidget ParentWidget { get; set; }
        public IWidget CurrentWidget { get; set; }
        public List<IWidget> Widgets { get; set; }

        public void ChangeConfigWidget<T>()
        {
            
        }

        public void UpdateData()
        {
            viewAnalazer.ItemsSource = (DataContext as user).analyzer.ToList();
        }
    }
}
