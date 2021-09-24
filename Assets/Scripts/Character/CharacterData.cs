using Components.Data;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Configs/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private float _baseHp;
        [SerializeField] private Battle.Character _characterPrefab;
        [SerializeField] private WeaponData _weaponData;
        public DataModel GetDataModel()
        {
            var dataModel = new DataModel();
            dataModel.AddProperty(Attributes.MaxHealth, _baseHp);
            dataModel.AddProperty(Attributes.CharacterPrefab, _characterPrefab);
            dataModel.AddProperty(Attributes.Weapon, _weaponData.GetWeapon());
            return dataModel;
        }
    }
}