using System;
using System.Collections.Generic;
using SecondBreath.Common.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.UI
{
    public class CanvasController: SerializedMonoBehaviour
    {
        public Transform this[CanvasLayer layer] => _layerParents.GetValue(layer);
        
        [SerializeField] private Canvas _canvas;
        [OdinSerialize] private Dictionary<CanvasLayer, Transform> _layerParents = new Dictionary<CanvasLayer, Transform>();

        public void CreateCanvasAndLayers()
        {
            CreateCanvas();
            CreateLayers();
        }

        private void CreateCanvas()
        {
            var canvasObject = new GameObject("Canvas");
            _canvas = canvasObject.AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            //todo add resolution
        }

        [Button]
        private void CreateLayers()
        {
            ClearLayers();

            foreach (var layer in Enum.GetValues(typeof(CanvasLayer)))
            {
                var layerParent = new GameObject(layer.ToString());
                layerParent.transform.SetParent(_canvas.transform);
                layerParent.transform.SetAsLastSibling();
                
                _layerParents.Add((CanvasLayer)layer, layerParent.transform);
            }
        }

        private void ClearLayers()
        {
            var childCount = gameObject.transform.childCount;
            var childTransforms = new List<Transform>();

            for (var i = 0; i < childCount; i++)
            {
                childTransforms.Add(transform.GetChild(i));
            }

            foreach (var childTransform in childTransforms)
            {
                DestroyImmediate(childTransform.gameObject);
            }
        }
    }
}