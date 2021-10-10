using SB.Core;
using SB.Helpers;
using UnityEngine;

namespace SB.Components
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
