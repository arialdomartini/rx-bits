using System;
using System.Linq;

namespace ReactiveBits.Delegates
{
    public class Delegates
    {
        // A delegate is a type that represents references to methods
        public delegate bool ComparisonDelegate(string a, string b);

        public static void ForEachint(int[] ints, Action<int> func)
        {
            ints.ToList().ForEach(func);
        }
    }
}