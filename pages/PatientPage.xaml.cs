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
    /// Логика взаимодействия для PatientPage.xaml
    /// </summary>
    public partial class PatientPage : Page
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
        private ViewBiomaterialsWidget viewBiomaterialsWidget;
        public List<ButtonAction> PatientActions;

        public PatientPage(user owner, Window parent)
        {
            InitializeComponent();
            DataContext = owner;
            parentWindow = parent;

            PatientActions = new List<ButtonAction>();
            PatientActions.Add(new ButtonAction("Просмотр биоматериалов", "document.png", ViewBiomaterials));

            viewBiomaterialsWidget = new ViewBiomaterialsWidget(owner, this);
        }

        private void ViewBiomaterials() => CurrentWidget = viewBiomaterialsWidget;

    }
}
