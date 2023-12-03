using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace BookShop
{


    public partial class SalesFavorite : Window
    {
        string st;

        //     List<BookBuy> BookB { get; set; }


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
                var result = db.favoritesPurchases.Include(x => x.User).Include(x => x.Book).Where(x => x.Status == st && x.User.Id == user1.Id).Select(x => new { x.Id, x.Book.Name, x.Book.SellingPrice }).ToList();
                // BookB = db.bookBuys.FromSqlInterpolated($"SELECT f.Id as 'SalesFavoriteID', b.name as 'Name', b.SellingPrice as 'Price'  FROM favoritesPurchases f  JOIN books b on b.Id=f.BookId  JOIN users u on u.Id=f.UserId   WHERE u.Id={user.Id} AND f.Status like '{st}'").ToList();
                //MessageBox.Show(result[0].Id.ToString(), "info", MessageBoxButton.OK);
                TableF.ItemsSource = result;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (st == "korzina")
            {

            }
            else
            {

            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (st == "korzina")
            {

            }
            else
            {

            }
        }
    }
}
