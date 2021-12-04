using UnityEngine;

namespace SecondBreath.Game.UI
{
    public class BaseView : MonoBehaviour, IView
    {
        protected object[] Data { get; private set; }

        public void Init(params object[] data)
        {
            Data = data;
            OnInit();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        protected virtual void OnInit()
        {
            
        }

        protected virtual void OnHide()
        {
            
        }
    }
}