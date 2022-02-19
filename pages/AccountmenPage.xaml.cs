using laboratory.common;
using laboratory.database;
using laboratory.interfaces;
using laboratory.widgets;
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
    public partial class AccountmenPage : Page, IPage, IAction
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
                Card.Content = _currentWidget;
            }
                
        }
        public Window ParentWindow { get; set; }
        public List<ButtonAction> Actions { get; set; }

        private ViewReportWidget viewReportWidget;
        private InvoiceGenerationWidget invoiceGenerationWidget;

        public AccountmenPage(user owner, Window parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentWindow = parent;

            Actions = new List<ButtonAction>();
            Actions.Add(new ButtonAction("Просмотр отчетов", "business_report.png", ViewReport));
            Actions.Add(new ButtonAction("Формирование счета", "document.png", InvoiceGeneration));

            viewReportWidget = new ViewReportWidget(owner, this);
            invoiceGenerationWidget = new InvoiceGenerationWidget(owner, this);
        }

        private void ViewReport() => CurrentWidget = viewReportWidget;
        private void InvoiceGeneration() => CurrentWidget = invoiceGenerationWidget;
    }
}
