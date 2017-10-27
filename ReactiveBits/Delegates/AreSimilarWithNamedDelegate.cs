using System;
using System.Collections.Generic;
using System.Linq;
using static ReactiveBits.Delegates.Delegates;

namespace ReactiveBits.Delegates
{
    public class AreSimilarWithNamedDelegate
    {
        public bool Check(IEnumerable<string> s1, IEnumerable<string> s2, ComparisonDelegate compare)
        {
            if (s1.Count() != s2.Count()) return false;

            var pairs = s1.Zip(s2, (item1, item2) => new Tuple<string, string>(item1, item2));

            return pairs.All(p => compare(p.Item1, p.Item2));
        }
    }

    public class AreSimilarWithFunc
    {
        public bool Check(IEnumerable<string> s1, IEnumerable<string> s2, Func<string, string, bool> compare)
        {
            if (s1.Count() != s2.Count()) return false;

            var pairs = s1.Zip(s2, (item1, item2) => new Tuple<string, string>(item1, item2));

            return pairs.All(p => compare(p.Item1, p.Item2));
        }
    }
}