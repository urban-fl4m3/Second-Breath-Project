﻿using SB.Components.Data;
using SB.Helpers;
using UnityEngine;

namespace SB.Battle
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Configs/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private float _baseHp;
        [SerializeField] private Character _characterPrefab;
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