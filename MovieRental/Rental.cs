namespace MovieRental
{
    public class Rental
    {
        private readonly int _daysRented;
        private readonly Movie _movie;

        public Rental(Movie movie, int daysRented)
        {
            _movie = movie;
            _daysRented = daysRented;
        }

        public int DaysRented
        {
            get { return _daysRented; }
        }

        public Movie Movie
        {
            get { return _movie; }
        }
    }
}