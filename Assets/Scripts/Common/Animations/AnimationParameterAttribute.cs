using System;
using UnityEngine;

namespace Common.Animations
{
    [AttributeUsage(AttributeTargets.Field  | AttributeTargets.Property)]
    public class AnimationParameterAttribute : Attribute
    {
        public AnimatorControllerParameterType Type { get; }
        public string AnimatorName { get; }
        
        public AnimationParameterAttribute(AnimatorControllerParameterType type, string animatorName)
        {
            Type = type;
            AnimatorName = animatorName;
        }
    }
}