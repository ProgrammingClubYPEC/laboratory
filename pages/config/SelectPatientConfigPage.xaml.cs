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
    internal partial class SelectedPatientItem
    {
        public SelectedPatientItem(bool isChecked, user service)
        {
            IsChecked = isChecked;
            Patient = service;
        }

        public bool IsChecked { get; set; }
        public user Patient { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для SelectPatientConfigPage.xaml
    /// </summary>
    public partial class SelectPatientConfigPage : Page, IPage
    {
        public Window ParentWindow { get; set; }
        public IWidget CurrentWidget { get; set; }
        public List<user> SelectedPatient { get; set; }
        private List<SelectedPatientItem> _selectedPatients;
        public SelectPatientConfigPage(user owner)
        {
            InitializeComponent();
            DataContext = owner;

            _selectedPatients = new List<SelectedPatientItem>();
            Instance.GetContext().user.Where(p => p.order.Any() && p.code_role == 4).ToList().ForEach(p => _selectedPatients.Add(new SelectedPatientItem(false, p)));
            patientComboBox.ItemsSource = _selectedPatients;
        }

        public void AppendPatient(List<user> patient)
        {
            _selectedPatients.ForEach(s =>
            {
                if (patient.Contains(s.Patient))
                    s.IsChecked = true;
            });
            patientComboBox.ItemsSource = _selectedPatients;
        }

        private void acceptSelectedPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedPatient = patientComboBox.ItemsSource.Cast<SelectedPatientItem>().Where(p => p.IsChecked).Select(p => p.Patient).ToList();
            ParentWindow.Close();
        }
    }
}
