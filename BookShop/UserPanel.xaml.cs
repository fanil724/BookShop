using Microsoft.EntityFrameworkCore;
using System.Windows;
namespace BookShop
{
    public partial class UserPanel : Window
    {
        public List<Book> b = new List<Book>() { };
        public User user1 { get; set; }

        public UserPanel(User user)
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

        private void NameChecked(object sender, RoutedEventArgs e)
        {
            avtor.IsChecked = false;
            ganr.IsChecked = false;
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

        private void addFavorit_Click(object sender, RoutedEventArgs e)
        {
            Book book = TableBookAdmin.SelectedItem as Book;

            if (book == null)
            {
                MessageBox.Show("Выбирите книгу для добавления", "Info", MessageBoxButton.OK);
                return;
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var rezult = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).FirstOrDefault(x => x.User.Id == user1.Id && x.Book.Id == book.Id && x.Status == "favorit");
                if (rezult != null)
                {
                    MessageBox.Show("Данная книга уже находиться в избранном", "Info", MessageBoxButton.OK);
                    return;
                }
                db.Attach(user1);
                db.Attach(book);
                db.favoritesPurchases.Add(new FavoritesPurchases { Book = book, Status = "favorit", User = user1 });
                db.SaveChanges();
            }
        }

        private void addKorzina_Click(object sender, RoutedEventArgs e)
        {
            Book book = TableBookAdmin.SelectedItem as Book;

            if (book == null)
            {
                MessageBox.Show("Выбирите книгу для добавления", "Info", MessageBoxButton.OK);
                return;
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var rezult = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).Where(x => x.User.Id == user1.Id && x.Book.Id == book.Id && x.Status == "korzina");
                if (rezult.Count() != 0)
                {
                    MessageBox.Show("Данная книга уже находиться в корзине", "Info", MessageBoxButton.OK);
                    return;
                }
                db.Attach(user1);
                db.Attach(book);
                FavoritesPurchases purchases = new FavoritesPurchases { Book = book, Status = "korzina", User = user1 };
                db.favoritesPurchases.Add(purchases);
                db.SaveChanges();
            }
        }

        private void NEWBOOK_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string years = DateTime.Now.Year.ToString();
                b = db.books.Where(x => x.PublicationYear == years).ToList();
                TableBookAdmin.ItemsSource = b;
            }
        }

        private void TopBuy_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                b = db.quantityAndSales.Include(x => x.Book).OrderByDescending(x => x.SalesBooks).Select(x => x.Book).ToList();
                TableBookAdmin.ItemsSource = b;
            }
        }

        private void Korzina_Click(object sender, RoutedEventArgs e)
        {
            SalesFavorite sl = new SalesFavorite(user1, "korzina");
            sl.Show();
        }

        private void favorites_Click(object sender, RoutedEventArgs e)
        {
            SalesFavorite sl = new SalesFavorite(user1, "favorit");
            sl.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
