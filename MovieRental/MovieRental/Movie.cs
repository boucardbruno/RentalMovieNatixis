namespace MovieRental.MovieRental
{
    public class Movie
    {
        public KindOfMovie KindOfMovie { get; private set; }
        public string Title { get; private set; }

        public Movie(string title, KindOfMovie kindOfMovie)
        {
            Title = title;
            KindOfMovie = kindOfMovie;
        }
    }
}
