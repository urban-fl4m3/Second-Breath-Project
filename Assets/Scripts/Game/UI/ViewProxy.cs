using UnityEngine;

namespace SecondBreath.Game.UI
{
    public class ViewProxy : IView
    {
        private readonly IView _view;
        private readonly GameObject _viewObject;
        
        public ViewProxy(IView view, ViewModel model, GameObject instance)
        {
            _view = view;
            _viewObject = instance;
        }
        
        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
            Object.Destroy(_viewObject);
        }
    }
}