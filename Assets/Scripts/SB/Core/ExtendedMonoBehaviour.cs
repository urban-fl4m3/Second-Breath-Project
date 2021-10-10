using UnityEngine;
using Zenject;

namespace SB.Core
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        [Inject] protected DiContainer _diContainer;
    }
}