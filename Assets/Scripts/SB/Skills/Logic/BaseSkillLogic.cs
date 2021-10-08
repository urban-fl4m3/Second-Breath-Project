using UnityEngine;

namespace SB.Skills.Logic
{
    public abstract class BaseSkillLogic
    {
        public abstract void SetData(BaseSkillData baseSkillData);
        public abstract void Activate(GameObject owner);
        public abstract void Deactivate();
    }
}