using UnityEngine;
using Zenject;

namespace SecondBreath.Game.UI
{
    public class ViewFactory
    {
        public CanvasController CanvasController 
        {
            get
            {
                if (_canvasController == null)
                {
                    var canvas = new GameObject("Canvas");
                    _canvasController = canvas.AddComponent<CanvasController>();
                    _canvasController.CreateCanvasAndLayers();
                }

                return _canvasController;
            }

            set => _canvasController = value;
        }
        
        private readonly DiContainer _container;
        
        private CanvasController _canvasController;

        public ViewFactory(DiContainer container)
        {
            _container = container;
        }
        
        public IView Create(ViewModel model)
        {
            var layer = model.Layer;
            var parent = CanvasController[layer];
            var viewInstance = _container.InstantiatePrefab(model.ViewObject, parent);
            var view = viewInstance.GetComponent<BaseView>();
            var proxy = new ViewProxy(view, model, viewInstance);
            view.Init(model.Data);
            return proxy;
        }
    }
}