using System.Windows;
using System.Windows.Media;

namespace BookShop
{
    public class UserEventArgs : EventArgs
    {
        public User us { get; set; }
        public UserEventArgs(User user)
        {
            us = user;
        }
    }

    public partial class Redaction : Window
    {
        public User use { get; set; }

        public event EventHandler<UserEventArgs> UserChanged;

        protected virtual void OnUserChanged(User user)
        {
            UserChanged?.Invoke(this, new UserEventArgs(user));
        }
        public Redaction() { }


        public Redaction(User user)
        {
            InitializeComponent();
            use = user;
            DataContext = use;
            OnePass.Password = use.Passowrd;
            TwoPass.Password = use.Passowrd;
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            if (OnePass.Password == TwoPass.Password && OnePass.Password != string.Empty)
            {
                use.Passowrd = OnePass.Password;
            }
            else
            {
                MessageBox.Show("Пароль не верный", "info", MessageBoxButton.OK);
                return;
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Update(use);
                db.SaveChanges();
            }

            OnUserChanged(use);

            this.Close();
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
