using UnityEngine;

namespace SecondBreath.Game.UI
{
    public readonly struct ViewModel
    {
        public readonly GameObject ViewObject;
        public readonly CanvasLayer Layer;
        public readonly object[] Data;

        public ViewModel(GameObject viewObject, CanvasLayer layer, params object[] data)
        {
            ViewObject = viewObject;
            Layer = layer;
            Data = data;
        }
    }
}