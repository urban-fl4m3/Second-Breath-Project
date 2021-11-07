using Zenject;

namespace SecondBreath.Common.Factories
{
    public class BaseFactory
    {
        protected DiContainer _container;
        
        public BaseFactory(DiContainer container)
        {
            _container = container;
        }
    }
}