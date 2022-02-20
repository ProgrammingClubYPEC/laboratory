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
    /// Логика взаимодействия для PatientPage.xaml
    /// </summary>
    public partial class PatientPage : Page, IPage, IAction
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

        public List<ButtonAction> Actions { get; set; }
        public Window ParentWindow { get; set; }

        private ViewBiomaterialsWidget viewBiomaterialsWidget;
        private LaboratoryNewsWidget laboratoryNewsWidget;

        public PatientPage(user owner, Window parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentWindow = parent;

            Actions = new List<ButtonAction>();
            Actions.Add(new ButtonAction("Новости", "newspaper.png", LaboratoryNews));
            Actions.Add(new ButtonAction("Просмотр биоматериалов", "document.png", ViewBiomaterials));

            viewBiomaterialsWidget = new ViewBiomaterialsWidget(owner, this);
            laboratoryNewsWidget = new LaboratoryNewsWidget(owner, this);
        }

        private void ViewBiomaterials() => CurrentWidget = viewBiomaterialsWidget;
        private void LaboratoryNews() => CurrentWidget = laboratoryNewsWidget;

    }
}
