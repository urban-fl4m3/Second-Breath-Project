using Common.Actors;
using SecondBreath.Common.Logger;
using Sirenix.OdinInspector;

namespace SecondBreath.Game.Battle.Characters.Components
{
    public class MovementComponent : SerializedMonoBehaviour, IActorComponent
    {
        public void Init(IDebugLogger logger)
        {
            logger.Log("MOVEMENT INIT");
        }
    }
}