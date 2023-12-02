using System.Windows;

namespace BookShop
{

    public partial class MainWindow : Window
    {
        User user;
        public MainWindow()
        {
            InitializeComponent();
        }



        private void Entrance_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                user = context.users.FirstOrDefault(x => x.Login == LoginText.Text);
                if (user == null)
                {
                    MessageBox.Show($"{LoginText.Text} такого пользователя не существует", "Info", MessageBoxButton.OK);
                    LoginText.Text = string.Empty;
                    PassText.Password = string.Empty;
                }
                else
                {
                    if (user.Passowrd == (VisPass.IsChecked.Value ? pwdTExt.Text : PassText.Password))
                    {
                        MessageBox.Show($"Поздравляю вы вошли как {user.Status}", "Info", MessageBoxButton.OK);

                        if (user.Status == "admin")
                        {
                            AdminPanel ad = new AdminPanel(user);
                            ad.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("НЕ верный пароль", "Info", MessageBoxButton.OK);
                        LoginText.Text = string.Empty;
                        PassText.Password = string.Empty;
                    }
                }
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void VisPass_Click(object sender, RoutedEventArgs e)
        {
            //using (ApplicationContext db = new ApplicationContext())
            //{
            //    User user = new User() { Name = "administrator", Login = "sisadmin", Passowrd = "proffi", Mail = "admin@mail.ru", Status = "admin", SurName = "administrator" };
            //    db.Add(user);
            //    db.SaveChanges();
            //}

            if (VisPass.IsChecked.Value)
            {
                pwdTExt.Text = PassText.Password;
                PassText.Visibility = Visibility.Hidden;
                pwdTExt.Visibility = Visibility.Visible;
            }
            else
            {
                PassText.Password = pwdTExt.Text;
                PassText.Visibility = Visibility.Visible;
                pwdTExt.Visibility = Visibility.Hidden;
            }

        }

        private void Grid_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Entrance_Click(sender, e);
            }
        }
    }
}