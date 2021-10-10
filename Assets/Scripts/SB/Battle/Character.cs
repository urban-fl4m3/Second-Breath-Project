using System.Collections.Generic;
using SB.Common.Attributes;
using SB.Core;
using SB.Helpers;

namespace SB.Battle
{
    public class Character : ExtendedMonoBehaviour
    {
        public IReadOnlyDictionary<AttributeType, RestrictedAttribute> Attributes => _attributes;
        
        private readonly Dictionary<AttributeType, RestrictedAttribute> _attributes 
            = new Dictionary<AttributeType, RestrictedAttribute>();
        
        public void Init(IReadOnlyDictionary<AttributeType, float> attributes)
        {
            gameObject.ActivateGameComponents();
            
            FillAttributes(attributes);
        }

        private void FillAttributes(IReadOnlyDictionary<AttributeType, float> attributes)
        {
            foreach (var attribute in attributes)
            {
                _attributes.Add(attribute.Key, new RestrictedAttribute(attribute.Value));
            }
        } 
    }
}