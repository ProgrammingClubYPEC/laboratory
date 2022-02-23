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
    public partial class AddingNewUserConfigWidget : Page, IConfigWidget
    {
        public IWidget ParentWidget { get; set; }

        private user NewPatient { get; set; }
        private user Laborant { get; set; }

        public AddingNewUserConfigWidget(user owner, IWidget parent)
        {
            InitializeComponent();
            Laborant = owner;
            NewPatient = new user();
            DataContext = NewPatient;
            ParentWidget = parent;
        }

        private void addPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            ParentWidget.ChangeConfigWidget<BiomaterialsOrderConfigWidget>();
        }
    }
}
