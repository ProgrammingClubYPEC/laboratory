using laboratory.common;
using laboratory.database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace laboratory.pages
{
    /// <summary>
    /// Логика взаимодействия для AccountmenPage.xaml
    /// </summary>
    public partial class AccountmenPage : Page
    {
        private Page _currentWidget;
        public Page CurrentWidget
        {
            get { return _currentWidget; }
            set
            {
                if (_currentWidget == value)
                    return;

                _currentWidget = value;
                OnPropertyChanged("CurrentWidget");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Window parentWindow;
        public List<ButtonAction> AccountmenActions;

        public AccountmenPage(user owner, Window parent)
        {
            InitializeComponent();
            DataContext = owner;
            parentWindow = parent;

            AccountmenActions = new List<ButtonAction>();
            AccountmenActions.Add(new ButtonAction("Просмотр отчетов", "business_report.png", ViewReport));
            AccountmenActions.Add(new ButtonAction("Формирование счета", "document.png", InvoiceGeneration));
        }

        private void ViewReport()
        {
            MessageBox.Show("view report");
        }

        private void InvoiceGeneration()
        {
            MessageBox.Show("invoice generation");
        }
    }
}
