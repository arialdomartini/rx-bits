using System.Runtime.ExceptionServices;

namespace ReactiveBits.Delegates
{
    public class Delegates
    {
        // A delegate is a type that represents references to methods
        public delegate bool ComparisonDelegate(string a, string b);
    }

    public class StringComparators
    {
        public static bool CompareContent(string a, string b)
        {
            return a == b;
        }

        public static bool CompareLength(string a, string b)
        {
            return a.Length == b.Length;
        }
    }
}