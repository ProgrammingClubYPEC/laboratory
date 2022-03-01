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
    /// Логика взаимодействия для AddingNewUserConfigWidget.xaml
    /// </summary>
    public partial class AddingNewUserConfigWidget : Page, IWidget, IFieldble
    {
        public IWidget ParentWidget { get; set; }

        private user NewPatient { get; set; }
        private user Laborant { get; set; }
        public IPage ParentPage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IWidget CurrentWidget { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IWidget> Widgets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AddingNewUserConfigWidget(user owner, IWidget parent)
        {
            InitializeComponent();
            Laborant = owner;

            UpdateData();
            user_role role = Instance.GetContext().user_role.Where(p => p.code_role.Equals(4)).First();
            NewPatient = new user()
            {
                login1 = new login(),
                user_confidential_data = new user_confidential_data() 
                {
                    policy_type = new policy_type(),
                    insurance_company = new insurance_company()
                },
                user_contact = new user_contact(),
                user_role = role,
                code_role = role.code_role
            };
            DataContext = NewPatient;
            ParentWidget = parent;
        }

        private void addPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            NewPatient.login = loginText.Text;
            Instance.GetContext().user.Add(NewPatient);
            Instance.GetContext().SaveChanges();
            
            (ParentWidget as IWidget).UpdateData();
            ParentWidget.ChangeConfigWidget<BiomaterialsOrderConfigWidget>();
        }

        public void ClearFields()
        {
            surnameText.Text = string.Empty;
            nameText.Text = string.Empty;
            midnameText.Text= string.Empty;
            bithdayDatepicker.SelectedDate = null;
            passroptSeriesText.Text = string.Empty;
            passroptIdText.Text = string.Empty;
            phoneText.Text = string.Empty;
            emailText.Text = string.Empty;
            insurancePolicyNumber.Text = string.Empty;
            insurancePolicyTypeText.Text = string.Empty;
            insuranceCompanyText.Text = string.Empty;
        }

        private void insurancePolicyTypeText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (insurancePolicyTypeText.SelectedItem as policy_type != null)
            {
                NewPatient.user_confidential_data.policy_type = insurancePolicyTypeText.SelectedItem as policy_type;
                NewPatient.user_confidential_data.insurance_policy_type_code = (insurancePolicyTypeText.SelectedItem as policy_type).policy_code;
            }
        }

        private void insuranceCompanyText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (insuranceCompanyText.SelectedItem as insurance_company != null)
            {
                NewPatient.user_confidential_data.insurance_company = insuranceCompanyText.SelectedItem as insurance_company;
                NewPatient.user_confidential_data.insurance_company_code = (insuranceCompanyText.SelectedItem as insurance_company).insurance_company_code;
            }    
        }

        public void UpdateData()
        {
            insuranceCompanyText.ItemsSource = Instance.GetContext().insurance_company.ToList();
            insurancePolicyTypeText.ItemsSource = Instance.GetContext().policy_type.ToList();
        }

        public void ChangeConfigWidget<T>()
        {
            throw new NotImplementedException();
        }
    }
}
