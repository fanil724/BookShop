using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace BookShop
{
    public class BookBuy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public double SellsPrice { get; set; }
        public double DiscPrice { get; set; }
    }

    public partial class SalesFavorite : Window
    {
        string st;
        double sumprice = 0;
        public List<BookBuy> books { get; set; } = new List<BookBuy>();

        User user1 { get; set; }
        public SalesFavorite(User user, string status)
        {
            InitializeComponent();
            if (status == "korzina")
            {
                AddOrBuy.Content = "Купить";
            }
            else
            {
                AddOrBuy.Content = "Отправить в корзину";
            }
            st = status;
            user1 = user;
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).Where(x => x.Status == st && x.User.Id == user1.Id).Select(x =>
                new { x.Book.Id, x.Book.Name, x.Book.Genre, x.Book.SellingPrice }).ToList();
                var disc = db.discounts.ToList();

                foreach (var book in result)
                {
                    books.Add(new BookBuy { ID = book.Id, Name = book.Name, Genre = book.Genre, SellsPrice = book.SellingPrice, DiscPrice = Math.Round(book.SellingPrice * Dscounte(book.Genre, disc), 2) });
                }
                sumprice = books.Sum(x => x.DiscPrice);
                Price.Text = Math.Round(sumprice, 2).ToString();
                TableF.ItemsSource = books;
            }
        }

        private double Dscounte(string gen, List<Discounts> ds)
        {
            foreach (var item in ds)
            {
                if (item.Genre == gen)
                {
                    return 1 - ((double)item.Discoute / 100);
                }
            }


            return 1d;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (st == "korzina")
            {
                List<FavoritesPurchases> fp = new List<FavoritesPurchases>();
                List<QuantityAndSales> qs = new List<QuantityAndSales>();
                using (ApplicationContext db = new ApplicationContext())
                {
                    foreach (var item in books)
                    {
                        fp.Add(db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).
                                FirstOrDefault(x => x.User.Id == user1.Id && x.Book.Id == item.ID && x.Status == st));
                        qs.Add(db.quantityAndSales.Include(x => x.Book).FirstOrDefault(x => x.Book.Id == item.ID));
                    }
                    foreach (var item in qs)
                    {
                        if (item.countBooks == 0)
                        {
                            MessageBox.Show($"{item.Book.Name} данной книги нет в наличии", "Info", MessageBoxButton.OK);
                            return;
                        }
                        item.countBooks--;
                        item.SalesBooks++;
                    }
                    db.RemoveRange(fp);
                    db.UpdateRange(qs);
                    db.SaveChanges();

                }
                MessageBox.Show("Поздравляю с покупкой", "Info", MessageBoxButton.OK);
                books.Clear();
                TableF.Items.Refresh();
            }
            else
            {
                BookBuy bookBuy = TableF.SelectedItem as BookBuy;
                if (bookBuy == null)
                {
                    MessageBox.Show("Выбирите запись", "Info", MessageBoxButton.OK);
                    return;
                }
                using (ApplicationContext db = new ApplicationContext())
                {
                    FavoritesPurchases result = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).
                        FirstOrDefault(x => x.User.Id == user1.Id && x.Book.Id == bookBuy.ID && x.Status == st);
                    if (result == null)
                    {
                        MessageBox.Show("Данной книги нет", "Info", MessageBoxButton.OK);
                        return;
                    }
                    result.Status = "korzina";
                    db.SaveChanges();
                    books.Remove(bookBuy);
                    TableF.Items.Refresh();
                }
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            BookBuy bookBuy = TableF.SelectedItem as BookBuy;
            if (bookBuy == null)
            {
                MessageBox.Show("Выбирите запись", "Info", MessageBoxButton.OK);
                return;
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                if (st == "korzina")
                {
                    FavoritesPurchases result = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).
                        FirstOrDefault(x => x.User.Id == user1.Id && x.Book.Id == bookBuy.ID && x.Status == st);
                    if (result == null)
                    {
                        MessageBox.Show("Данной книги нет", "Info", MessageBoxButton.OK);
                        return;
                    }
                    db.favoritesPurchases.Remove(result);
                }
                else
                {
                    FavoritesPurchases result = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).
                        FirstOrDefault(x => x.User.Id == user1.Id && x.Book.Id == bookBuy.ID && x.Status == st);
                    if (result == null)
                    {
                        MessageBox.Show("Данной книги нет", "Info", MessageBoxButton.OK);
                        return;
                    }
                    db.favoritesPurchases.Remove(result);
                }
                db.SaveChanges();
                MessageBox.Show("Запись удалена", "Info", MessageBoxButton.OK);
            }
            books.Remove(bookBuy);
            sumprice = books.Sum(x => x.DiscPrice);
            Price.Text = Math.Round(sumprice, 2).ToString();
            TableF.Items.Refresh();
        }
    }
}