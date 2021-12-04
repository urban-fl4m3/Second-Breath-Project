using System;
using UnityEngine;

namespace SecondBreath.Game.UI
{
    [Serializable]
    public struct ViewData
    {
        [SerializeField] private GameObject _viewObject;
        [SerializeField] private CanvasLayer _layer;

        public GameObject ViewObject => _viewObject;
        public CanvasLayer Layer => _layer;
    }
}