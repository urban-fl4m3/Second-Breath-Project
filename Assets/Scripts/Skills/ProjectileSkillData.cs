using Components.Data;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(fileName = "ProjectileSkillData", menuName = "Configs/ProjectileSkillData")]
    public class ProjectileSkillData : BaseSkillData
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _projectilesCount;
        [SerializeField] private float _projectileDamage;

        public override DataModel GetDataModel()
        {
            var dataModel = new DataModel();
            dataModel.AddProperty(Attributes.Damage, _projectileDamage);
            dataModel.AddProperty(Attributes.ProjectileCount, _projectilesCount);
            dataModel.AddProperty(Attributes.ProjectilePrefab, _projectilePrefab);
            
            return dataModel;
        }
    }
}