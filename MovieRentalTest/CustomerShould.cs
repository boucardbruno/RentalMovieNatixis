using MovieRental.MovieRental;
using NFluent;
using NUnit.Framework;

namespace MovieRentalTest
{
    public static class CustomerShould
    {
        [TestCase("Guest", 0)]
        public static void Return_Statement_when_no_rental(string customerName, int amount)
        {
            var customer = new Customer(customerName);

            Check.That(customer.Statement()).IsEqualTo(
                $"Rental Record for {customerName}\nAmount owed is {amount}\nYou earned {amount} frequent renter points");
        }

        [TestCase("Dracula Untold", "Thomas", 2)]
        public static void Return_Statement_when_rental_of_one_regular_movie_during_less_than_2_days(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.Regular, 1, customerName);

            Check.That(customer.Statement()).IsEqualTo(
                $"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 1 frequent renter points");
        }

        [TestCase("Dracula Untold", "Eric", 3.5)]
        public static void Return_Statement_when_rental_of_one_regular_movie_during_more_than_2_days(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.Regular, 3, customerName);

            Check.That(customer.Statement()).IsEqualTo(
                $"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 1 frequent renter points");
        }

        [TestCase("Dracula Untold", "Pierre", 6)]
        public static void Return_Statement_when_rental_of_one_new_release_movie_during_more_than_one_day(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.NewRelease, 2, customerName);

            Check.That(customer.Statement()).IsEqualTo(
                $"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 2 frequent renter points");
        }

        [TestCase("Dracula Untold", "Bruno", 3)]
        public static void Return_Statement_when_rental_of_one_new_release_movie_during_less_than_one_day(string movieName, string customerName, double amount)
        {
            var customer = MakeCustomerWithRental(movieName, KindOfMovie.NewRelease, 1, customerName);

            Check.That(customer.Statement())
                .IsEqualTo($"Rental Record for {customerName}\n\t{movieName}\t{amount}\nAmount owed is {amount}\nYou earned 1 frequent renter points");
        }

        [Test]
        public static void Return_Statement_when_rental_of_one_children_movie_during_less_than_3_days()
        {
            const string amazingSpiderMan = "The Amazing Spider-Man";
            const string bruno = "Bruno";
            var customer = MakeCustomerWithRental(amazingSpiderMan, KindOfMovie.Children, 1, bruno);

            Check.That(customer.Statement()).IsEqualTo(
                $"Rental Record for {bruno}\n\t{amazingSpiderMan}\t1,5\nAmount owed is 1,5\nYou earned 1 frequent renter points");
        }

        [Test]
        public static void Return_Statement_when_rental_of_one_children_movie_during_more_than_3_days()
        {
            const string amazingSpiderMan = "The Amazing Spider-Man";
            const string bruno = "Bruno";
            var customer = MakeCustomerWithRental(amazingSpiderMan, KindOfMovie.Children, 4, bruno);

            Check.That(customer.Statement())
                .IsEqualTo(
                    $"Rental Record for {bruno}\n\t{amazingSpiderMan}\t3\nAmount owed is 3\nYou earned 1 frequent renter points");
        }

        [Test]
        public static void Return_Statement_when_multiple_rentals()
        {
            var theAmazingSpiderMan = "The Amazing Spider-Man";
            var pride = "Pride";
            var draculaUntold = "Dracula Untold";
            var bruno = "Bruno";
            var customer = MakeCustomerWithRental(theAmazingSpiderMan, KindOfMovie.Children, 4, bruno);
            customer.AddRental(new Rental(new Movie(pride, KindOfMovie.Regular), 2));
            customer.AddRental(new Rental(new Movie(draculaUntold, KindOfMovie.NewRelease), 3));

            Check.That(customer.Statement()).IsEqualTo(
                $"Rental Record for {bruno}\n\t{theAmazingSpiderMan}\t3\n\t{pride}\t2\n\t{draculaUntold}\t9\nAmount owed is 14\nYou earned 4 frequent renter points");
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
