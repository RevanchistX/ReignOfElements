using UnityEngine;

namespace Scenes
{
    public class TankMovement : MonoBehaviour
    {
        private Transform body;
        private Transform turret;
        private Transform gun;
        private Camera camera;

        void Start()
        {
            body = transform.Find("Body");
            turret = transform.Find("Turret");
            gun = turret.Find("Gun");
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            HandleGunRotation(turret);
        }

        private void HandleGunRotation(Transform transformer)
        {
            var mousePosition = Input.mousePosition;
            var worldRay = camera.ScreenPointToRay(mousePosition);
            // Debug.DrawRay(worldRay.origin, worldRay.direction * 100, Color.red);
            transformer.rotation = Quaternion.LookRotation(worldRay.direction);
        }
    }
}