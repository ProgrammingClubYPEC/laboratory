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
    internal partial class SelectedServiceItem
    {
        public SelectedServiceItem(bool isChecked, service service)
        {
            IsChecked = isChecked;
            Service = service;
        }

        public bool IsChecked { get; set; }
        public service Service { get; set; }
    }

    /// <summary>
    /// Логика взаимодействия для SelectServiceConfigPage.xaml
    /// </summary>
    public partial class SelectServiceConfigPage : Page, IPage
    {
        public Window ParentWindow { get; set; }
        public IWidget CurrentWidget { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<service> SelectedServices { get; set; }
        private List<SelectedServiceItem> _selectedServices;

        public SelectServiceConfigPage(user owner)
        {
            InitializeComponent();
            DataContext = owner;

            _selectedServices = new List<SelectedServiceItem>();
            owner.service.ToList().ForEach(service => _selectedServices.Add(new SelectedServiceItem(false, service)));
            serviceComboBox.ItemsSource = _selectedServices;
        }

        public void AppendServices(List<service> services)
        {
            _selectedServices.ForEach(s =>
            {
                if (services.Contains(s.Service))
                    s.IsChecked = true;
            });
            serviceComboBox.ItemsSource = _selectedServices;
        }

        private void acceptSelectedServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedServices = serviceComboBox.ItemsSource.Cast<SelectedServiceItem>().Where(p => p.IsChecked).Select(p => p.Service).ToList();
            ParentWindow.Close();
        }
    }
}
