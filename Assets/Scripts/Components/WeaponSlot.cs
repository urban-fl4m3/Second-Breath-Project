using Helpers;
using UnityEngine;

namespace Components
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private Transform _weaponSlot;
        private DataHolder _data;

        private void Start()
        {
            _data = this.GetOrAddComponent<DataHolder>();
            _data.Properties.AddProperty(Attributes.HandTransform, _weaponSlot);
        }
    }
}
