using UnityEngine;
using UnityEngine.UI;

namespace SB.UI
{
    [CreateAssetMenu(fileName = "ViewProvider", menuName = "Configs/ViewProvider")]
    public class ViewProvider : ScriptableObject
    {
        [SerializeField] private Image _healthBar;

        public Image HealthBar => _healthBar;
    }
}