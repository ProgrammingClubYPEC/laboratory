using laboratory.database;
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
    /// Логика взаимодействия для UserCardWidget.xaml
    /// </summary>
    public partial class UserCardWidget : Page
    {
        public UserCardWidget(user owner)
        {
            InitializeComponent();
            DataContext = owner;
            initializeIcon(owner.user_role);
        }

        private void initializeIcon(user_role role)
        {
            List<string> roles = Instance.GetContext().user_role.Select(p => p.name).ToList();
            string image = string.Empty;
            if (roles.ElementAt(0).Equals(role.name))
                image = "laborant_2.png";
            else if (roles.ElementAt(1).Equals(role.name))
                image = "accountmen.jpeg";
            else if (roles.ElementAt(2).Equals(role.name))
                image = "admin.png";
            else
                image = "icons/default_user.png";
            userImage.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath($"../../Resources/{image}")));
        }
    }
}
