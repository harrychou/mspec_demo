using System.Collections.Generic;

namespace mspec_demo
{
    public class PotterBookComparer : IEqualityComparer<PotterBook>
    {
        public bool Equals(PotterBook x, PotterBook y)
        {
            return x.Title.Equals(y.Title);
        }

        public int GetHashCode(PotterBook obj)
        {
            return obj.Title.GetHashCode();
        }
    }
}