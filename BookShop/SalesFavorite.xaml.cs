﻿using Microsoft.EntityFrameworkCore;
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
                new { x.Id, x.Book.Name, x.Book.Genre, x.Book.SellingPrice }).ToList();
                var disc = db.discounts.ToList();

                foreach (var book in result)
                {
                    books.Add(new BookBuy { ID = book.Id, Name = book.Name, Genre = book.Genre, SellsPrice = book.SellingPrice, DiscPrice = Math.Round(book.SellingPrice * Dscounte(book.Genre, disc), 2) });
                }
                double sumprice = books.Sum(x => x.DiscPrice);
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
