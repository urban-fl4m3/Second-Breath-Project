using Cinemachine;
using SB.Core;
using UnityEngine;

namespace SB.Managers
{
    public class CameraManager : ExtendedMonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _mainCamera;

        public void SetMainCameraTarget(Transform target)
        {
            _mainCamera.Follow = target;
        }
    }
}