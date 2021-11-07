using SecondBreath.Common.Factories;
using Zenject;

namespace SecondBreath.Common.States
{
    public class StateFactory : BaseFactory, IStateFactory
    {
        public StateFactory(DiContainer container) : base(container)
        {
            
        }


        public IState GetState<TState>() where TState : IState
        {
            var obj = _container.Instantiate(typeof(TState));
            return (IState)obj;
        }
    }
}