using SB.Components.Data;
using UnityEngine;

namespace SB.Skills.Logic
{
    public abstract class BaseSkillLogic
    {
        public abstract void SetData(DataModel dataModel);
        public abstract void Activate(GameObject owner);
        public abstract void Deactivate();
    }
}