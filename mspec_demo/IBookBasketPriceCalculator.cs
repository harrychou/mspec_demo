using System.Collections.Generic;

namespace mspec_demo
{
    public interface IBookBasketPriceCalculator {
        double CalculateBookPrice(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer);
    }
}