using System.Windows;

namespace BookShop
{
  
    public partial class Dicsount : Window
    {
        public List<Discounts> discounts { get; set; } = new List<Discounts>();
        public Dicsount()
        {
            InitializeComponent();
            using (ApplicationContext db = new ApplicationContext())
            {
                discounts = db.discounts.ToList();
            }
            TableDisc.ItemsSource = discounts;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.discounts.UpdateRange(discounts);
                db.SaveChanges();
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {

            if (TableDisc.SelectedIndex != -1)
            {
                Discounts d = TableDisc.SelectedItem as Discounts;
                if (discounts.Remove(d))
                {
                    MessageBox.Show("yes", "info", MessageBoxButton.OK);
                    TableDisc.Items.Refresh();
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.discounts.Remove(d);
                        db.SaveChanges();
                    }
                }

            }
            else
            {
                MessageBox.Show("Выбирите элемент для удаления", "Инфо", MessageBoxButton.OK);
            }

        }
    }
}
