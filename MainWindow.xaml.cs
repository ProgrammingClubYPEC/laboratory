using laboratory.common;
using laboratory.database;
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
    public class ButtonAction :INotifyPropertyChanged
    {
        public ImageSource ImageSource { get; set; }
        public string NameAction { get; set; }

        private Visibility _visibility;
        public Visibility NameActionVisibly { 
            get { return _visibility; } 
            set
            {
                _visibility = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NameActionVisibly"));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public ButtonAction(string name, string image)
        {
            NameAction = name;
            ImageSource = new BitmapImage(new Uri(System.IO.Path.GetFullPath($"../../Resources/icons/{image}")));
            NameActionVisibly = Visibility.Collapsed;
        }
    }

    public partial class MainWindow : Window
    {
        private Page _userCardCurrentWidget;
        public Page UserCardCurrentWidget
        {
            get { return _userCardCurrentWidget; }
            set
            {
                if (_userCardCurrentWidget == value)
                    return;

                _userCardCurrentWidget = value;
                OnPropertyChanged("UserCardCurrentWidget");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<ButtonAction> actionButtons;

        public MainWindow(user owner)
        {
            InitializeComponent();
            DataContext = owner;
            initialize();
            userCard.Content = new UserCardWidget(owner);
            initializeActions(owner.user_role);
            
        }

        private void initializeActions(user_role role)
        {
            actionButtons = new List<ButtonAction>();
            for (int i = 0; i < 3; i++)
            {
                ButtonAction btn = new ButtonAction($"Button {i}", "document.png");
                actionButtons.Add(btn);
            }

            listUserAction.ItemsSource = actionButtons;
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

                //listUserAction.ItemsSource.OfType<ButtonAction>().ToList().ForEach(btn => btn.NameActionVisibly = Visibility.Visible);
                menu.Width = 150;
            }
            else if (userCard.Visibility == Visibility.Visible)
            {
                userCard.Visibility = Visibility.Collapsed;
                menuText.Visibility = Visibility.Collapsed;
                exitText.Visibility = Visibility.Collapsed;
                actionButtons.ToList().ForEach(p => p.NameActionVisibly = Visibility.Collapsed);
                //listUserAction.ItemsSource.OfType<ButtonAction>().ToList().ForEach(btn => btn.NameActionVisibly = Visibility.Collapsed);
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
            
        }
    }
}
