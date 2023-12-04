using System.Windows;
namespace BookShop
{

    public partial class AdminPanel : Window
    {
        public List<Book> b = new List<Book>() { };
        public User user1 { get; set; }


        public AdminPanel(User user)
        {
            InitializeComponent();
            use.Header = user.Login;
            nam.IsChecked = true;
            user1 = user;
            Redaction r = new Redaction();
            r.UserChanged += OnUserChaenged;
        }

        private void OnUserChaenged(object sender, UserEventArgs e)
        {
            user1 = e.us;
            use.Header = user1.Login;
        }

        private void AVTOR_Checked_1(object sender, RoutedEventArgs e)
        {
            ganr.IsChecked = false;
            nam.IsChecked = false;
        }

        private void GANR_Checked_2(object sender, RoutedEventArgs e)
        {
            avtor.IsChecked = false;
            nam.IsChecked = false;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.UpdateRange(b);
                db.SaveChanges();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            stringSearch.Text = string.Empty;
            using (ApplicationContext db = new ApplicationContext())
            {
                b = db.books.ToList();
                TableBookAdmin.ItemsSource = b;
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (nam.IsChecked == true)
                {
                    b = db.books.Where(x => x.Name.StartsWith(stringSearch.Text)).ToList();

                }

                if (avtor.IsChecked == true)
                {
                    b = db.books.Where(x => x.FIOAvtora.StartsWith(stringSearch.Text)).ToList();
                }

                if (ganr.IsChecked == true)
                {
                    b = db.books.Where(x => x.Genre.StartsWith(stringSearch.Text)).ToList();
                }
            }
            TableBookAdmin.ItemsSource = b;
        }

        private void RED_Click(object sender, RoutedEventArgs e)
        {
            Redaction redaction = new Redaction(user1);
            redaction.Show();
        }

        private void EXIT_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
        }

        private void NameChecked(object sender, RoutedEventArgs e)
        {
            avtor.IsChecked = false;
            ganr.IsChecked = false;
        }

        private void Dics_Click(object sender, RoutedEventArgs e)
        {
            Dicsount discount = new Dicsount();
            discount.Show();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Book book = TableBookAdmin.CurrentCell.Item as Book;

            if (book == null)
            {
                MessageBox.Show("Выбирите строчку для удаления", "Info", MessageBoxButton.OK);
                return;
            }


            using (ApplicationContext db = new ApplicationContext())
            {
                db.books.Remove(book);
                db.SaveChanges();
            }
        }
    }
}
