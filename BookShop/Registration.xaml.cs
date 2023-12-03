using System.Windows;
using System.Windows.Media;

namespace BookShop
{
    public partial class Registration : Window
    {
        User user = new User() { Status = "user" };
        public Registration()
        {
            InitializeComponent();
            DataContext = user;
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            if (OnePass.Password == TwoPass.Password && OnePass.Password != string.Empty)
            {
                user.Passowrd = OnePass.Password;
            }
            else
            {
                MessageBox.Show("Пароль не верный", "info", MessageBoxButton.OK);
                return;
            }

            if (user.IsNull())
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.users.Add(user);
                    db.SaveChanges();
                }
                UserPanel userPanel = new UserPanel(user);
                userPanel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполинте все поля", "info", MessageBoxButton.OK);
                return;
            }


        }

        private void TwoPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (OnePass.Password != TwoPass.Password)
            {
                TwoPass.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            }
            else
            {
                TwoPass.Background = new SolidColorBrush(Color.FromRgb(191, 255, 0));
            }
        }
    }
}
