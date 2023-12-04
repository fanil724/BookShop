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

        }

        private void addKorzina_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NEWBOOK_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.books.Select(x => x.NameOfThePublisher == DateTime.Now.Year.ToString()).ToList();
                TableBookAdmin.ItemsSource = result;
            }
        }

        private void TopBuy_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.quantityAndSales.Include(x => x.Book).OrderByDescending(x => x.SalesBooks).Select(x => new { x.Book.Id, x.Book.Name, x.Book.Genre, x.SalesBooks }).ToList();
                TableBookAdmin.ItemsSource = result;
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
    }
}
