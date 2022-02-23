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

namespace laboratory.widgets.config
{
    /// <summary>
    /// Логика взаимодействия для BiomaterialsOrderConfigWidget.xaml
    /// </summary>
    public partial class BiomaterialsOrderConfigWidget : Page, IConfigWidget
    {
        public IWidget ParentWidget { get; set; }

        private user Patient { get; set; }
        private List<service> Services { get; set; }

        public BiomaterialsOrderConfigWidget(user owner, IWidget parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentWidget = parent;

            Services = new List<service>();

            removingService.IsEnabled = false;
            patientComboBox.SelectedIndex = -1;
            patientComboBox.ItemsSource = Instance.GetContext().user.Where(p => p.code_role.Equals(4)).ToList();
            servicesListBox.ItemsSource = Services;
        }

        private void addingNewPatient_Click(object sender, RoutedEventArgs e)
        {
            ParentWidget.ChangeConfigWidget<AddingNewUserConfigWidget>();
        }

        private void addingService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removingService_Click(object sender, RoutedEventArgs e)
        {
            if (servicesListBox.SelectedItem != null && Services.Contains(servicesListBox.SelectedItem))
                Services.Remove(servicesListBox.SelectedItem as service);
        }

        private void patientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient = patientComboBox.SelectedItem as user;
        }

        private void servicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removingService.IsEnabled = true;
        }

        private void addingOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentWidget.UpdateData();
        }
    }
}
