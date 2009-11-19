 
using System;


namespace mspec_demo
{
    public class PotterBook: IEquatable<PotterBook>
    {
        string title;
    
        PotterBook(int i)
        {
            title = "Potter Book " + i;
        }

        public string Title
        {
            get { return title; }
        }

        static public PotterBook CreateBook(int i)
        {
            return new PotterBook(i);
        }

        public bool Equals(PotterBook other)
        {
            return this.Title.Equals(other.Title);
        }

    }
}