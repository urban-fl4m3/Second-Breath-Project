using System;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Common.Animations.Editor
{
    public class AnimationParameterAttributeDrawer : OdinAttributeDrawer<AnimationParameterAttribute, string>
    {
        private Animator _animator;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (_animator == null)
            {
                var error = string.Empty;
                
                try
                {
                    var animatorValue = ValueResolver.Get<Animator>(Property, Attribute.AnimatorName);
                    _animator = animatorValue.GetValue();
                    error = animatorValue.ErrorMessage;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Something went wrong! Err msg: '{error}'; Exception msg: '{e.Message}'");
                    CallNextDrawer(label);
                    return;
                }
            }

            ValueEntry.SmartValue = SirenixEditorFields.Dropdown(label, ValueEntry.SmartValue, GetNames());
        }

        private string[] GetNames()
        {
            return _animator.parameters?
                .Where(p => p.type == Attribute.Type)
                .Select(p => p.name)
                .ToArray();
        }
    }
}