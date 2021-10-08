using Cinemachine;
using UnityEngine;

namespace SB.Managers
{
    public class CameraManager : InjectManager
    {
        private CinemachineVirtualCamera _mainCamera;

        public void AddMainVirtualCamera(CinemachineVirtualCamera camera)
        {
            _mainCamera = camera;
        }
        
        public void SetMainCameraTarget(Transform target)
        {
            _mainCamera.Follow = target;
        }
    }
}