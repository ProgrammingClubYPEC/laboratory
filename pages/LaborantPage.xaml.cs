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
    /// Логика взаимодействия для LaborantPage.xaml
    /// </summary>
    public partial class LaborantPage : Page, IPage, IAction
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

        private ReportGenerationWidget reportGenerationWidget;
        private IntakeBiomaterialsWidget biomaterialsWidget;

        public LaborantPage(user owner, Window parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentWindow = parent;

            Actions = new List<ButtonAction>();
            Actions.Add(new ButtonAction("Формирование отчетов", "business_report.png", ReportGeneration));
            Actions.Add(new ButtonAction("Прием биоматериалов", "document.png", IntakeBiomaterials));

            reportGenerationWidget = new ReportGenerationWidget(owner, this);
            biomaterialsWidget = new IntakeBiomaterialsWidget(owner, this);
        }

        private void ReportGeneration() => CurrentWidget = reportGenerationWidget;
        private void IntakeBiomaterials() => CurrentWidget = biomaterialsWidget;
    }
}
