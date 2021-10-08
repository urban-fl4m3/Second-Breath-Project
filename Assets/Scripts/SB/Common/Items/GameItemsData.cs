using UnityEngine;

namespace SB.Common.Items
{
    [CreateAssetMenu(fileName = "GameItemsData", menuName = "Configs/Items/GameItemData")]
    public class GameItemsData : ScriptableObject
    {
        [SerializeField] private ItemMaterial _itemMaterial;

        public ItemMaterial ItemMaterial => _itemMaterial;
    }
}