using laboratory.common;
using laboratory.database;
using laboratory.pages;
using laboratory.widgets;
using System;
using System.Collections;
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
using System.Windows.Input.StylusPlugIns;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace laboratory
{
    public partial class MainWindow : Window
    {
        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value)
                    return;

                _currentPage = value;
                page.Content = _currentPage;
            }
        }

        public List<ButtonAction> actionButtons;
        private List<string> userRolesList;

        public MainWindow(user owner)
        {
            InitializeComponent();
            DataContext = owner;
            userCard.Content = new UserCardWidget(owner);
            
            initialize();
            
            CurrentPage = createPage(owner);
        }

        private Page createPage(user owner)
        {
            userRolesList = Instance.GetContext().user_role.Select(p => p.name).ToList();
            Page page;
            if (owner.user_role.name.Equals(userRolesList.ElementAt(0)))
            {
                page = new LaborantPage(owner, this);
                actionButtons = (page as LaborantPage).LaborantActions;
            }
            else if (owner.user_role.name.Equals(userRolesList.ElementAt(1)))
            {
                page = new AccountmenPage(owner, this);
                actionButtons = (page as AccountmenPage).AccountmenActions;
            }
            else if (owner.user_role.name.Equals(userRolesList.ElementAt(2)))
            {
                page = new AdminPage(owner, this);
                actionButtons = (page as AdminPage).AdminActions;
            }
            else
            {
                page = new PatientPage(owner, this);
                actionButtons = (page as PatientPage).PatientActions;
            }
            
            listUserAction.ItemsSource = actionButtons;
            return page;
        }

        private void initialize()
        {
            menuIcon.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/icons/menu.png")));
            exitIcon.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath("../../Resources/icons/power_off.png")));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Show();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            if (userCard.Visibility == Visibility.Collapsed)
            {
                userCard.Visibility = Visibility.Visible;
                menuText.Visibility = Visibility.Visible;
                exitText.Visibility = Visibility.Visible;
                actionButtons.ToList().ForEach(p => p.NameActionVisibly = Visibility.Visible);
                menu.Width = 150;
            }
            else if (userCard.Visibility == Visibility.Visible)
            {
                userCard.Visibility = Visibility.Collapsed;
                menuText.Visibility = Visibility.Collapsed;
                exitText.Visibility = Visibility.Collapsed;
                actionButtons.ToList().ForEach(p => p.NameActionVisibly = Visibility.Collapsed);
                menu.Width = 50;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти из учетной записи?", "Выход из приложения", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Close();
        }

        private void listUserAction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actionButtons.ElementAt(listUserAction.SelectedIndex).Action();
        }
    }
}
