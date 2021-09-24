using Core;
using UnityEngine;

namespace Helpers
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this MonoBehaviour self) where T : Component
        {
            var component = self.gameObject.GetComponent<T>();

            if (component == null)
                component = self.gameObject.AddComponent<T>();

            return component;
        }
        
        public static void ActivateGameComponents(this GameObject self)
        {
            var gameComponents = self.GetComponents<GameComponent>();

            foreach (var item in gameComponents)
            {
                item.Activate();
            }
            
        }
    }
}