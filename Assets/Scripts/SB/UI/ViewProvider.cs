using UnityEngine;

namespace SB.UI
{
    [CreateAssetMenu(fileName = "ViewProvider", menuName = "Configs/ViewProvider")]
    public class ViewProvider : ScriptableObject
    {
        //change later to game object and create MVC
        [SerializeField] private HealthBarView _healthBar;

        public HealthBarView HealthBar => _healthBar;
    }
}