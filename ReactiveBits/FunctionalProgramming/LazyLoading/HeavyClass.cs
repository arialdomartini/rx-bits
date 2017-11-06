namespace ReactiveBits.FunctionalProgramming.LazyLoading
{
    public class HeavyClass
    {
        // Suppose this class takes a lot of time to be create
        public string GetAString()
        {
            return "foo";
        }
    }
}