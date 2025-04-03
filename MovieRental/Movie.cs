namespace MovieRental
{
    public class Movie
    {       

        private readonly string _title;

        public Movie(string title, KindOfMovie priceCode)
        {
            _title = title;
            PriceCode = priceCode;
        }

        public KindOfMovie PriceCode { get; set; }

        public string Title
        {
            get { return _title; }
        }
    }
}