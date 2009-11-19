using System.Collections.Generic;


namespace mspec_demo
{
    public class PotterBookBasket
    {
        readonly IEqualityComparer<PotterBook> _comparer;
        List<PotterBook> _books;
        readonly IBookBasketPriceCalculator _priceCalculator;

        public PotterBookBasket(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer, IBookBasketPriceCalculator calculator)
        {
            _comparer = comparer;
            _priceCalculator = calculator;
            _books = new List<PotterBook>(books);
        }

        public PotterBookBasket(IEnumerable<PotterBook> books): this(books, new PotterBookComparer(), new BookBasketPriceCalculator())
        {
            _books = new List<PotterBook>(books);
        }

        public double CalculatePrice()
        {
            return _priceCalculator.CalculateBookPrice(_books, _comparer);
        }
    }
}