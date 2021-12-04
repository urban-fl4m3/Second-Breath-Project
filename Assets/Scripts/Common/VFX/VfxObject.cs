using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.VFX
{
    public class VfxObject : MonoBehaviour
    {
        [SerializeField] private float _defaultScale;

        [SerializeField] private List<GameObject> vfxObjects = new List<GameObject>();

        private void Awake()
        {
            foreach (var vfx in vfxObjects)
            {
                vfx.transform.localScale = new Vector3(_defaultScale, _defaultScale, _defaultScale);
            }
        }

        public void UpdateScale(float newRadius)
        {
            float scaleCoef = newRadius / 1.0f;
            foreach (var vfx in vfxObjects)
            {
                vfx.transform.localScale = new Vector3(_defaultScale * scaleCoef, _defaultScale * scaleCoef, _defaultScale * scaleCoef);
            }
        }
    }
}