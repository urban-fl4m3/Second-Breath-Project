using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardVec = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, forwardVec);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed);
    }
}
