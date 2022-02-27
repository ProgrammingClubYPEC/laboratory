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
    public partial class AddingNewUserConfigWidget : Page, IConfigWidget, IFieldble
    {
        public IWidget ParentWidget { get; set; }

        private user NewPatient { get; set; }
        private user Laborant { get; set; }

        public AddingNewUserConfigWidget(user owner, IWidget parent)
        {
            InitializeComponent();
            Laborant = owner;

            user_role role = Instance.GetContext().user_role.Where(p => p.code_role.Equals(4)).First();
            NewPatient = new user()
            {
                login1 = new login(),
                user_confidential_data = new user_confidential_data(),
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
    }
}
