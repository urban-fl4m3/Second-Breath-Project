using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    private void OnCollisionEnter2D()
    {
        Destroy(gameObject);
    }
}
