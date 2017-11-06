using System;

namespace ReactiveBits.FunctionalProgramming.LazyLoading
{
    public class LightClassWithLazy
    {
        private readonly Lazy<HeavyClass> _lazyHeavyClass;

        public LightClassWithLazy(Lazy<HeavyClass> lazyHeavyClass)
        {
            _lazyHeavyClass = lazyHeavyClass;
        }

        public string SomeMethod()
        {
            return _lazyHeavyClass.Value.GetAString();
        }
    }
}