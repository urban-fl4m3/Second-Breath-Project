using Components.Data;
using UnityEngine;

namespace Skills.Logic
{
    public abstract class BaseSkillLogic
    {
        public abstract void SetData(DataModel dataModel);
        public abstract void Activate(GameObject owner);
        public abstract void Deactivate();
    }
}