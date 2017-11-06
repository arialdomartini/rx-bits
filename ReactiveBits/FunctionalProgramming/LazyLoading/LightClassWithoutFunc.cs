namespace ReactiveBits.FunctionalProgramming.LazyLoading
{
    public class LightClassWithoutFunc
    {
        internal HeavyClass HeavyClassInstance;

        private HeavyClass HeavyClass => HeavyClassInstance ?? (HeavyClassInstance = new HeavyClass());

        public string SomeMethod()
        {
            return HeavyClass.GetAString();
        }
    }
}