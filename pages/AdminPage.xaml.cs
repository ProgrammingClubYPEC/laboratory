using laboratory.common;
using laboratory.database;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
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

        private Window parentWindow;
        private ViewReportWidget viewReportWidget;
        private ConsumableDataProcessingWidget consumableDataProcessingWidget;
        private ViewAuthorizationHistoryWidget viewAuthorizationHistoryWidget;

        public List<ButtonAction> AdminActions;

        public AdminPage(user owner, Window parent)
        {
            InitializeComponent();
            DataContext = owner;
            parentWindow = parent;

            AdminActions = new List<ButtonAction>();
            AdminActions.Add(new ButtonAction("Просмотр отчетов", "business_report.png", ViewReport));
            AdminActions.Add(new ButtonAction("Просмотр расходных материалов", "warehouse.png", ConsumableDataProcessing));
            AdminActions.Add(new ButtonAction("Просмотр истории авторизации", "history.png", ViewAuthorizationHistory));

            viewReportWidget = new ViewReportWidget(owner, this);
            consumableDataProcessingWidget = new ConsumableDataProcessingWidget(owner, this);
            viewAuthorizationHistoryWidget = new ViewAuthorizationHistoryWidget(owner, this);
        }

        private void ViewReport() => CurrentWidget = viewReportWidget;
        private void ConsumableDataProcessing() => CurrentWidget = consumableDataProcessingWidget;
        private void ViewAuthorizationHistory() => CurrentWidget = viewAuthorizationHistoryWidget;

    }
}
