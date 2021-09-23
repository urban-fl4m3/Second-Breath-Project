using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Core;
using UnityEngine;

public class CameraManager : ExtendedMonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainCamera;

    public void SetMainCameraTarget(Transform target)
    {
        _mainCamera.Follow = target;
    }
}
