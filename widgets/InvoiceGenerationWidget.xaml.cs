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

namespace laboratory.widgets
{
    /// <summary>
    /// Логика взаимодействия для InvoiceGenerationWidget.xaml
    /// </summary>
    public partial class InvoiceGenerationWidget : Page, IWidget, IErrorMessage, IFieldble
    {
        public IPage ParentPage { get; set; }
        public IWidget ParentWidget { get; set; }
        public IWidget CurrentWidget { get; set; }
        public List<IWidget> Widgets { get; set; }
        private string _errorMessageString;
        public string MessageErrorString
        {
            get { return _errorMessageString; }
            set
            {
                _errorMessageString = value;
                ErrorMessageText.Text = value;
            }
        }
        private ObservableCollection<user> Patients;
        private ObservableCollection<service> Services;

        public InvoiceGenerationWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;
            Patients = new ObservableCollection<user>();
            Services = new ObservableCollection<service>();
            removePatientBtn.IsEnabled = false;
            companyComboBox.SelectedIndex = -1;
            UpdateData();
        }

        public void UpdateData()
        {
            companyComboBox.ItemsSource = Instance.GetContext().insurance_company.ToList();

            foreach (user user in Patients)
                foreach (order order in user.order)
                {
                    if (!Services.Contains(order.service))
                        Services.Add(order.service);
                }
            serviceGrid.ItemsSource = Services;
        }

        public void ChangeConfigWidget<T>()
        {
        }

        private void addInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (companyComboBox.SelectedIndex == -1 || startDatePicker.SelectedDate == null || endDatePicker == null || !Patients.Any())
            {
                ShowMessageErrorString("Необходимо чтобы все поля были выбраны и заполнены.");
                return;
            }
            insurance_company company = companyComboBox.SelectedItem as insurance_company;

            GenerateOrderPdf(company.name, (DateTime)startDatePicker.SelectedDate, (DateTime)endDatePicker.SelectedDate, Patients);
            ShowMessageErrorString("Электронный вариант заказа успешно сохранен в /Resources/pdf");
            ClearFields();
        }

        private void GenerateOrderPdf(string nameCompany, DateTime start, DateTime end, ObservableCollection<user> patients)
        {
            GeneratePDF generator = new GeneratePDF();
            generator.WriteTextLine($"Company: {nameCompany}");
            generator.WriteTextLine($"Period of: {start.Date.ToString()} - {end.Date.ToString()}");
            decimal totalSum = 0;
            foreach (user user in patients)
            {
                string nameService = string.Empty;
                decimal total = 0;
                foreach(order o in user.order)
                {
                    nameService += $"{o.service.name}; ";
                    total += o.service.price;
                }

                generator.WriteTextLine($"Patient: {user.ToString()};");
                generator.WriteTextLine($"Services: {nameService}; Sum: {total} rub.");
                totalSum += total;
            }
            generator.WriteTextLine($"Total sum: {totalSum} rub.");
            generator.Save($"invoice-{nameCompany}");
        }

        private void removePatientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (patientListBox.SelectedIndex != -1)
            {
                Patients.Remove(patientListBox.SelectedItem as user);
                patientListBox.ItemsSource = Patients;
                UpdateTotalSum();
            }
        }

        private void UpdateTotalSum()
        {
            decimal totalSum = 0;
            foreach (var p in Patients)
                foreach (var o in p.order)
                    totalSum += o.service.price;

            totalSumText.Text = $"{totalSum}";
        }

        private void addPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectPatientConfigPage selectPatientConfigPage = new SelectPatientConfigPage(DataContext as user);
            CommonWindow window = new CommonWindow(selectPatientConfigPage);
            selectPatientConfigPage.ParentWindow = window;
            selectPatientConfigPage.AppendPatient(Patients.ToList());

            if (!(bool)window.ShowDialog())
            {
                Patients = new ObservableCollection<user>(selectPatientConfigPage.SelectedPatient);
                patientListBox.ItemsSource = Patients;
                UpdateTotalSum();
                UpdateData();
            }
        }

        public async void ShowMessageErrorString(string errorString)
        {
            MessageErrorString = errorString;
            await Task.Delay(5000);
            MessageErrorString = string.Empty;
        }

        public void ClearFields()
        {
            Patients.Clear();
            endDatePicker.SelectedDate = null;
            startDatePicker.SelectedDate = null;
            serviceGrid.ItemsSource = null;
            companyComboBox.SelectedIndex = -1;
            removePatientBtn.IsEnabled = false;
            patientListBox.ItemsSource = null;
            UpdateTotalSum();
        }

        private void patientListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            removePatientBtn.IsEnabled = true;
        }
    }
}
