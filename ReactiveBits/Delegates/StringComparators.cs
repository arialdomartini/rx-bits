namespace ReactiveBits.Delegates
{
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