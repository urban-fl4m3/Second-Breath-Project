using Common.Actors;
using UnityEngine;

namespace Common.Animations
{
    public class BaseAnimatorController : ActorComponent
    {
        [SerializeField] private Animator _animator;
    }
}