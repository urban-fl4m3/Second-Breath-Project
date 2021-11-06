using System.Collections;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Characters.Configs;
using UnityEngine;
using Zenject;

namespace Game.Battle
{
    public class BattleInitializer : MonoInstaller
    {
        public override void InstallBindings()
        {
            StartCoroutine(AfterBindingsInstalled());
        }

        private IEnumerator AfterBindingsInstalled()
        {
            yield return new WaitForEndOfFrame();
         
            var config = Container.Resolve<BattleCharactersConfig>();
            var logger = Container.Resolve<IDebugLogger>();
            
            logger.Log(config.ToString());
        }
    }
}