using UnityEngine;

public class Movement : MonoBehaviour
{
    private void FixedUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X");

        var gameObjectTransform = transform;
        gameObjectTransform.Rotate(Vector3.up * mouseX);
    }
}