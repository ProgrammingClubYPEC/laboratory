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

namespace laboratory.pages.config
{
    /// <summary>
    /// Логика взаимодействия для AnalyzeBiomaterialsPage.xaml
    /// </summary>
    public partial class AnalyzeBiomaterialsPage : Page, IPage
    {
        private user Laborant { get; set; }
        private analyzer Analyzer { get; set; }

        public AnalyzeBiomaterialsPage(user owner, analyzer analyzer)
        {
            InitializeComponent();
            Laborant = owner;
            Analyzer = analyzer;
            ordersComboBox.ItemsSource = Instance.GetContext().order.Where(p => p.employeer_login.Equals(Laborant.login) && p.order_status.value < 50).ToList();
            acceptSelectedServiceBtn.IsEnabled = false;
        }

        public Window ParentWindow { get; set; }
        public IWidget CurrentWidget { get; set; }

        private void ordersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            acceptSelectedServiceBtn.IsEnabled = true;
        }

        private void acceptSelectedServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            order order = ordersComboBox.SelectedItem as order;
            if (order == null)
                return;

            rendered_type type = Instance.GetContext().rendered_type.Where(p => p.render_type.Equals(2)).FirstOrDefault();
            Random random = new Random();
            Instance.GetContext().rendered.Add(new rendered()
            {
                analyzer_code = Analyzer.analyzer_code,
                emploee_login = Laborant.login,
                patient_login = order.customer_login,
                render_type = type.render_type,
                date_of_service = order.creation_date,
                service_code = order.service_code,
                order = order,
                order_code = order.order_code,
                result = random.Next(0, 100),

                analyzer = Analyzer,
                rendered_type = type,
                service = order.service,
                user1 = order.user,
                user = Laborant
            });

            order.order_status.value = 50;
            order.service_status.value = 50;

            Instance.GetContext().SaveChanges();
            Instance.Reload();
            ParentWindow.Close();
        }
    }
}
