using laboratory.common;
using laboratory.database;
using laboratory.interfaces;
using laboratory.pages.config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BiomaterialsOrderConfigWidget : Page, IWidget, IFieldble
    {
        public IWidget ParentWidget { get; set; }

        private user Patient { get; set; }
        private ObservableCollection<service> Services { get; set; }
        public IPage ParentPage { get; set; }
        public IWidget CurrentWidget { get; set; }
        public List<IWidget> Widgets { get; set; }

        public BiomaterialsOrderConfigWidget(user owner, IWidget parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentWidget = parent;

            Services = new ObservableCollection<service>();

            removingService.IsEnabled = false;
            patientComboBox.SelectedIndex = -1;
            UpdateData();
        }

        private void addingNewPatient_Click(object sender, RoutedEventArgs e)
        {
            ParentWidget.ChangeConfigWidget<AddingNewUserConfigWidget>();
        }

        private void addingService_Click(object sender, RoutedEventArgs e)
        {
            SelectServiceConfigPage selectServiceConfigPage = new SelectServiceConfigPage(DataContext as user);
            CommonWindow window = new CommonWindow(selectServiceConfigPage);
            selectServiceConfigPage.ParentWindow = window;
            selectServiceConfigPage.AppendServices(Services.ToList());
            
            if (!(bool)window.ShowDialog())
            {
                Services = new ObservableCollection<service>(selectServiceConfigPage.SelectedServices);
                servicesListBox.ItemsSource = Services;
                UpdateTotalSum();
            }
        }

        private void removingService_Click(object sender, RoutedEventArgs e)
        {
            if (servicesListBox.SelectedIndex != -1)
            {
                Services.Remove(servicesListBox.SelectedItem as service);
                servicesListBox.ItemsSource = Services;
                UpdateTotalSum();
            }
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
            if (Patient == null)
            {
                (ParentWidget as IErrorMessage).ShowMessageErrorString("Пациент не выбран!");
                return;
            }
            if (!Services.Any())
            {
                (ParentWidget as IErrorMessage).ShowMessageErrorString("Услуги не выбраны!");
                return;
            }
            if (String.IsNullOrEmpty(tubeCodeText.Text))
            {
                (ParentWidget as IErrorMessage).ShowMessageErrorString("Номер пробирки должен быть установлен!");
                return;
            }

            biomaterials_tube tube = new biomaterials_tube()
            {
                tube_code = Instance.GetContext().biomaterials_tube.ToList().Count() + 1,
                name = $"{Patient.surname} {Patient.name.First()}.{(Patient.midname != null ? Patient.midname.First() : ' ')}. login:{Patient.login}"
            };
            Instance.GetContext().biomaterials_tube.Add(tube);
            
            string orderName = $"{Patient.surname}: ";
            Services.ToList().ForEach(s => orderName += $"{s.name}; ");
            order_status status = new order_status()
            {
                status_code = Instance.GetContext().order_status.ToList().Count() + 1,
                name = orderName,
                value = 0
            };
            Instance.GetContext().order_status.Add(status);

            string serviceListName = string.Empty;
            Services.ToList().ForEach(s => serviceListName += $"{s.name}; ");
            service_status statusService = new service_status()
            {
                status_code = Instance.GetContext().service_status.ToList().Count() + 1,
                name = serviceListName,
                value = 0
            };
            Instance.GetContext().service_status.Add(statusService);
            
            foreach (service service in Services.ToList())
            {
                Instance.GetContext().order.Add(new order()
                {
                    creation_date = DateTime.Now,
                    customer_login = Patient.login,
                    service_code = service.service_code,
                    order_status_code = status.status_code,
                    service_status_code = statusService.status_code,
                    days_to_complete = 3,
                    tube_code = tube.tube_code,
                    employeer_login = (DataContext as user).login,

                    order_status = status,
                    service = service,
                    service_status = statusService,
                    user = Patient,
                    user1 = DataContext as user,
                    biomaterials_tube = tube
                });
            }    

            Instance.GetContext().SaveChanges();
            ParentWidget.UpdateData();

            order order = Instance.GetContext().order.ToList().Last();

            GenerateOrderPdf(order.order_code, order.creation_date, tubeCodeText.Text, order.user, serviceListName, totalSumText.Text);
            (ParentWidget as IErrorMessage).ShowMessageErrorString("Электронный вариант заказа успешно сохранен в /Resources/pdf");
            (ParentWidget as IFieldble).ClearFields();
        }

        private void GenerateOrderPdf(long n, DateTime date, string barcode, user patient, string services, string totalsum)
        {
            GeneratePDF generator = new GeneratePDF();
            generator.WriteTextLine($"Order {n}");
            generator.WriteTextLine($"Creation date order: {date.Date.ToString()}");
            generator.WriteTextLine($"Tube code: {barcode}");
            generator.WriteTextLine($"Full name: {patient.ToString()}");
            generator.WriteTextLine($"Bithday: {patient.bithday.ToString()}");
            generator.WriteTextLine($"Insurance policy number: {patient.user_confidential_data.insurance_policy_number}");
            generator.WriteTextLine($"Services: {services}");
            generator.WriteTextLine($"Total sum: {totalsum}");
            generator.Save($"order-{n}-{patient.login}");
        }

        private void UpdateTotalSum()
        {
            decimal totalSum = 0;
            foreach (var service in Services)
                totalSum += service.price;

            totalSumText.Text = $"{totalSum}";
        }

        public void ClearFields()
        {
            Patient = null;
            Services.Clear();
            patientComboBox.SelectedIndex = -1;
            removingService.IsEnabled = false;
            servicesListBox.ItemsSource = null;
            tubeCodeText.Text = String.Empty;
            UpdateTotalSum();
        }

        public void UpdateData()
        {
            tubeCodeText.Text = $"{Instance.GetContext().biomaterials_tube.ToList().Count() + 1}";
            patientComboBox.ItemsSource = Instance.GetContext().user.Where(p => p.code_role.Equals(4)).ToList();
            servicesListBox.ItemsSource = Services;
        }

        public void ChangeConfigWidget<T>()
        {
        }

        private void editPatient_Click(object sender, RoutedEventArgs e)
        {
            ParentWidget.ChangeConfigWidget<AddingNewUserConfigWidget>();
        }
    }
}
