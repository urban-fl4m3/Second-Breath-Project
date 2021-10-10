using UnityEngine;

namespace SB.Skills
{
    [CreateAssetMenu(fileName = "ProjectileSkillData", menuName = "Configs/ProjectileSkillData")]
    public class ProjectileSkillData : BaseSkillData
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _projectilesCount;
        [SerializeField] private float _projectileDamage;

        public GameObject ProjectilePrefab => _projectilePrefab;
        public float ProjectilesCount => _projectilesCount;
        public float ProjectileDamage => _projectileDamage;
    }
}