using System.Collections;
using UnityEngine;
using Zenject;

namespace SecondBreath.Common.Initializer
{
    public abstract class BaseSceneInitializer : MonoInstaller
    {
        public override void InstallBindings()
        {
            StartCoroutine(AfterBindingsInstalled());
        }
        
        protected abstract void OnAwake();

        private IEnumerator AfterBindingsInstalled()
        {
            yield return new WaitForEndOfFrame();
            OnAwake();
        }
    }
}