using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.UI
{
    [CreateAssetMenu(fileName = nameof(ViewProvider), menuName = "Configs/UI/View Provider")]
    public class ViewProvider : SerializedScriptableObject
    {
        [OdinSerialize] private ViewData _characterSelectionViewData;

        public ViewData CharacterSelectionViewData => _characterSelectionViewData;
    }
}