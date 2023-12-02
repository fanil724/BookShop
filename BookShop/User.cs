namespace BookShop
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Passowrd { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Mail { get; set; }
        public string? Status { get; set; }


        public bool IsNull()
        {
            if (null == Login) return false;
            if (null == Passowrd) return false;
            if (null == Name) return false;
            if (null == SurName) return false;
            if (null == Mail) return false;

            return true;
        }
    }   

    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FIOAvtora { get; set; }
        public string? NameOfThePublisher { get; set; }
        public int PageNumber { get; set; }
        public string Genre { get; set; }
       
        public string? PublicationYear { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }

        public string? BookContinuation { get; set; }
    }


    public class FavoritesPurchases
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public string? Status { get; set; }

    }

    public class Discounts
    {
        public int Id { get; set; }
        public string Genre { get; set; }
        public int Discoute { get; set; }
    }


}
