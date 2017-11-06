using System;

namespace ReactiveBits.FunctionalProgramming.LazyLoading
{
    public class LightClassWithFunc
    {
        private readonly Func<HeavyClass> _heavyClassFactory;

        public LightClassWithFunc(Func<HeavyClass> heavyClassFactory)
        {
            _heavyClassFactory = heavyClassFactory;
        }

        public string SomeMethod()
        {
            return _heavyClassFactory().GetAString();
        }
    }
}