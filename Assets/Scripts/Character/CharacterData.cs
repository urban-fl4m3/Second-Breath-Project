using Components.Data;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Configs/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private float baseHp;
        [SerializeField] private Battle.Character CharacterPrefab;
        public DataModel GetDataModel()
        {
            var dataModel = new DataModel();
            dataModel.AddProperty(Attributes.MaxHealth, baseHp);
            dataModel.AddProperty(Attributes.CharacterPrefab, CharacterPrefab);
            
            return dataModel;
        }
    }
}