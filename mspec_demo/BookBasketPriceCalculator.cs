using System.Collections.Generic;
using System.Linq;

namespace mspec_demo
{
    public class BookBasketPriceCalculator : IBookBasketPriceCalculator {
        const double UNIT_PRICE = 8;

        public double CalculateBookPrice(IEnumerable<PotterBook> books, IEqualityComparer<PotterBook> comparer)
        {
            IList<string> titles = new List<string>(books.Select(book => book.Title));

            var rootNode = new TitleCalculatorTreeNode(null,
                                                       new TreeNodeItem {grouping = new List<string>(), items_to_calculate = titles},
                                                       GetVolumnDiscountFor, UNIT_PRICE);

            IDictionary<string, double> priceLookUp = new Dictionary<string, double>();
            return rootNode.CalculatePrice(priceLookUp);
        }

        static double GetVolumnDiscountFor(int count)
        {
            if (count == 1) return 1;
            if (count == 2) return .95;
            if (count == 3) return .9;
            if (count == 4) return .8;
            return .75;
        }

    }
}