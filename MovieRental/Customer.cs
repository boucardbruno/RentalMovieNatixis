using System.Collections.Generic;

namespace MovieRental
{
    public class Customer
    {
        private readonly string _name;
        private readonly List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string Statement()
        {
            double totalAmount = 0;
            var frequentRenterPoints = 0;

            var result = "Rental Record for " + Name + "\n";
            foreach (var each in _rentals)
            {
                double amount = 0;

                //determine amounts for each line
                switch (each.Movie.PriceCode)
                {
                    case KindOfMovie.Regular:
                        amount += 2;
                        if (each.DaysRented > 2)
                            amount += (each.DaysRented - 2)*1.5;
                        break;
                    case KindOfMovie.NewRelease:
                        amount += each.DaysRented*3;
                        break;
                    case KindOfMovie.Children:
                        amount += 1.5;
                        if (each.DaysRented > 3)
                            amount += (each.DaysRented - 3)*1.5;
                        break;
                }
                var thisAmount = amount;
                // add frequent renter points
                frequentRenterPoints ++;
                // add bonus for a two day new release rental
                if ((each.Movie.PriceCode == KindOfMovie.NewRelease) &&
                    each.DaysRented > 1) frequentRenterPoints ++;
                //show figures for this rental
                result += "\t" + each.Movie.Title + "\t" +
                          thisAmount + "\n";
                totalAmount += thisAmount;
            }
            //add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints +
                      " frequent renter points";
            return result;
        }
    }
}