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

namespace laboratory.widgets
{
    /// <summary>
    /// Логика взаимодействия для ViewAuthorizationHistoryWidget.xaml
    /// </summary>
    public partial class ViewAuthorizationHistoryWidget : Page, IWidget
    {
        public IPage ParentPage { get; set; }
        public IWidget ParentWidget { get; set; }
        public IWidget CurrentWidget { get; set; }
        public List<IWidget> Widgets { get; set; }

        public ViewAuthorizationHistoryWidget(user owner, IPage parent)
        {
            InitializeComponent();
            DataContext = owner;
            ParentPage = parent;
            Initilize();
            UpdateData();
        }

        private void Initilize()
        {
            attemptFilterComboBox.ItemsSource = new List<string>() { "None", "True", "False" };
            loginFilterComboBox.ItemsSource = Instance.GetContext().login.Select(p => p.login1.Trim()).ToList();
        }

        public void UpdateData()
        {
            var historyList = Instance.GetContext().login_history.OrderByDescending(p => p.id_history).ToList();

            if (loginFilterComboBox.SelectedIndex > 0)
                historyList = historyList.Where(p => p.login.Trim().Contains(loginFilterComboBox.SelectedItem as string)).ToList();

            if (serachDatePicker.SelectedDate != null)
                historyList = historyList.Where(p => p.last_data_time.Date.Equals(serachDatePicker.SelectedDate)).ToList();

            if (attemptFilterComboBox.SelectedIndex > 0)
                historyList= historyList.Where(p => p.attempt.ToString().Equals(attemptFilterComboBox.SelectedItem as string)).ToList();

            historyTable.ItemsSource = historyList;
        }

        private void serachDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void loginFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void attemptFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        public void ChangeConfigWidget<T>()
        {
        }
    }
}
