using Core;
using Helpers;
using UnityEngine;

namespace Components
{
    public class WeaponSlot : GameComponent
    {
        [SerializeField] private Transform _weaponSlot;
        private DataHolder _data;

        public override void Activate()
        {
            _data = this.GetOrAddComponent<DataHolder>();
            _data.Properties.AddProperty(Attributes.HandTransform, _weaponSlot);
        }
    }
}
