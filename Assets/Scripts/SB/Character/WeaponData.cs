using UnityEngine;

namespace SB.Character
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Configs/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private Weapon _weapon;

        public Weapon GetWeapon()
        {
            return Instantiate(_weapon);
        }
    }
}