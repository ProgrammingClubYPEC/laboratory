using laboratory.database;
using laboratory.interfaces;
using laboratory.pages.config;
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
        rendered_type fail;
        rendered_type success;
        public ResearchAnalazerWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;
            fail = Instance.GetContext().rendered_type.Where(p => p.render_type == 4).FirstOrDefault();
            success = Instance.GetContext().rendered_type.Where(p => p.render_type == 3).FirstOrDefault();
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
            string login = (DataContext as user).login;
            viewAnalazer.ItemsSource = Instance.GetContext().analyzer.Where(p => p.responsible_employee_login.Contains(login.Trim())).ToList();
            renderedServicesGrid.ItemsSource = Instance.GetContext().rendered.Where(p => p.emploee_login.Contains(login.Trim()) && p.render_type == 2).ToList();
        }

        private void analazeBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag == null)
                return;
            long index = (long)(sender as Button).Tag;
            analyzer analyzer = Instance.GetContext().analyzer.Where(p => p.analyzer_code == index).FirstOrDefault();

            AnalyzeBiomaterialsPage analazeConfigPage = new AnalyzeBiomaterialsPage(DataContext as user, analyzer);
            CommonWindow window = new CommonWindow(analazeConfigPage);
            analazeConfigPage.ParentWindow = window;

            if (!(bool)window.ShowDialog())
            {
                UpdateData();
            }
        }

        private void acceptResultsBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Tag == null)
                return;
            long index = (long)(sender as Button).Tag;
            rendered rendered = Instance.GetContext().rendered.Where(p => p.render_code == index).FirstOrDefault();

            MessageBoxResult result = MessageBox.Show($"Корректны ли результаты {rendered.result} для {rendered.service.name}?", "Принятие результатов", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                rendered.render_type = success.render_type;
                rendered.rendered_type = success;
            }
            else if (result == MessageBoxResult.No)
            {
                rendered.render_type = fail.render_type;
                rendered.rendered_type = fail;
            }

            rendered.order.order_status.value = 100;
            rendered.order.service_status.value = 100;

            Instance.GetContext().SaveChanges();
            Instance.Reload();
            UpdateData();
        }
    }
}
