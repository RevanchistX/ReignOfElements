using UnityEngine;

public class Movement : MonoBehaviour
{

    private Camera _camera;

    private void Awake()
    {
        _camera = (Camera)FindObjectOfType(typeof(Camera));
    }

    private void FixedUpdate()
    {
        var mouseX = Input.GetAxis("Mouse X") * 2f;

        var gameObjectTransform = transform;
        gameObjectTransform.Rotate(Vector3.up * mouseX);
        var cameraTransform = _camera.transform;
        cameraTransform.LookAt(gameObjectTransform);
        cameraTransform.rotation = gameObjectTransform.rotation;
    }
}