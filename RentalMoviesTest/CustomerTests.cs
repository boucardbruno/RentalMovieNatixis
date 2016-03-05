using NFluent;
using NUnit.Framework;
using RentalMovies;
using static System.String;
namespace RentalMoviesTest
{
    public static class CustomerTests
    {
        [TestCase("Guest", 0)]
        public static void Should_return_Statement_when_no_rental(string customerName, int amount)
        {
            var customer = new Customer(customerName);

            Check.That(customer.Statement()).Equals(
                Format($"Rental Record for {customerName}\nAmount owed is {amount}\nYou earned {amount} frequent renter points"));
        }

        [TestCase("Dracula Untold", "Thomas", 2)]
        public static void Should_return_Statement_when_rental_of_one_regular_movie_during_less_than_2_days(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.Regular, 1, customerName);

            Check.That(customer.Statement()).Equals(
                Format($"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 1 frequent renter points"));
        }

        [TestCase("Dracula Untold", "Rui", 3.5)]
        public static void Should_return_Statement_when_rental_of_one_regular_movie_during_more_than_2_days(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.Regular, 3, customerName);

            Check.That(customer.Statement()).Equals(
                Format($"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 1 frequent renter points"));
        }

        [TestCase("Dracula Untold", "Arnauld", 6)]
        public static void Should_return_Statement_when_rental_of_one_new_release_movie_during_more_than_one_day(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.NewRelease, 2, customerName);

            Check.That(customer.Statement()).Equals(
                Format($"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 2 frequent renter points"));
        }

        [TestCase("Dracula Untold", "Bruno", 3)]
        public static void Should_return_Statement_when_rental_of_one_new_release_movie_during_less_than_one_day(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.NewRelease, 1, customerName);

            Check.That(customer.Statement())
                .Equals(Format("Rental Record for {0}\n\t{1}\t{2}\nAmount owed is {2}\nYou earned 1 frequent renter points", customerName, movieName, amount));
        }

        [Test]
        public static void Should_return_Statement_when_rental_of_one_children_movie_during_less_than_3_days()
        {
            const string movieName = "The Amazing Spider-Man";
            const string customerName = "Bruno";
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.Children, 1, customerName);

            Check.That(customer.Statement()).Equals(
                Format($"Rental Record for {customerName}\n\t{movieName}\t1,5\nAmount owed is 1,5\nYou earned 1 frequent renter points"));
        }

        [Test]
        public static void Should_return_Statement_when_rental_of_one_children_movie_during_more_than_3_days()
        {
            const string movieName = "The Amazing Spider-Man";
            const string customerName = "Bruno";
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.Children, 4, customerName);

            Check.That(customer.Statement())
                .Equals(
                    $"Rental Record for {customerName}\n\t{movieName}\t3\nAmount owed is 3\nYou earned 1 frequent renter points");
        }

        [Test]
        public static void Should_return_Statement_when_multiple_rentals()
        {
            var movie1 = "The Amazing Spider-Man";
            var movie2 = "Pride";
            var movie3 = "Dracula Untold";
            var customername = "Bruno";
            var customer = MakeCustomerWithRental(movie1, KindOfMovie.Children, 4, customername);
            customer.AddRental(new Rental(new Movie(movie2, KindOfMovie.Regular), 2));
            customer.AddRental(new Rental(new Movie(movie3, KindOfMovie.NewRelease), 3));

            Check.That(customer.Statement()).Equals(
                Format($"Rental Record for {customername}\n\t{movie1}\t3\n\t{movie2}\t2\n\t{movie3}\t9\nAmount owed is 14\nYou earned 4 frequent renter points"));
        }

        private static Customer MakeCustomerWithRental(string movieName, KindOfMovie priceCode, int daysRented, string mycustomername)
        {
            var rental = new Rental(new Movie(movieName, priceCode), daysRented);
            var customer = new Customer(mycustomername);
            customer.AddRental(rental);
            return customer;
        }
    }
}
