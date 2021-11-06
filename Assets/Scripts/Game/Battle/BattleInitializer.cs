using SecondBreath.Common.Logger;
using SecondBreath.Game.Characters.Configs;
using Zenject;

namespace Game.Battle
{
    public class BattleInitializer : MonoInstaller
    {
        private void Start()
        {
            var config = Container.Resolve<BattleCharactersConfig>();
            var logger = Container.Resolve<ILogger>();
            
            logger.Log(config.ToString());
        }
    }
}