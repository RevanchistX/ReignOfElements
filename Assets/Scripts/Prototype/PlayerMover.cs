using System;
using Prototype;
using UnityEngine;
using UnityEngine.UIElements;

namespace AdvancedController
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerMover : MonoBehaviour
    {
        #region Fields

        [Header("Collider Settings:")]
        [Range(0f, 1f)]
        [SerializeField]
        float stepHeightRatio = 0.1f;

        [SerializeField]
        float colliderHeight = 2f;

        [SerializeField]
        float colliderThickness = 1f;

        [SerializeField]
        Vector3 colliderOffset = Vector3.zero;

        Rigidbody rb;
        Transform tr;
        CapsuleCollider col;
        RaycastSensor sensor;

        bool isGrounded;
        float baseSensorRange;
        Vector3 currentGroundAdjustmentVelocity; // Velocity to adjust player position to maintain ground contact
        int currentLayer;

        [Header("Sensor Settings:")]
        [SerializeField]
        bool isInDebugMode;

        bool isUsingExtendedSensorRange = true; // Use extended range for smoother ground transitions

        #endregion


        [SerializeField, Range(0.001f, 1f)]
        private float timeScale = 1f;

        void Awake()
        {
            Setup();
            RecalculateColliderDimensions();
        }

        void OnValidate()
        {
            if (gameObject.activeInHierarchy)
            {
                RecalculateColliderDimensions();
            }
        }

        void LateUpdate()
        {
            Time.timeScale = timeScale;
            // if (sensor.HasDetectedHit()) Time.timeScale = 0;
            if (isInDebugMode)
            {
                sensor.DrawDebug();
            }
        }

        public void CheckForGround()
        {
            if (currentLayer != gameObject.layer)
            {
                RecalculateSensorLayerMask();
            }

            currentGroundAdjustmentVelocity = Vector3.zero;
            sensor.CastLength = isUsingExtendedSensorRange
                ? baseSensorRange + colliderHeight * tr.localScale.x * stepHeightRatio
                : baseSensorRange;
            // sensor.CastLength = 5f;
            sensor.Cast();

            isGrounded = sensor.HasDetectedHit();
            // Debug.Log($"isGrounded {isGrounded}");
            if (!isGrounded) return;

            float distance = sensor.GetDistance();
            float upperLimit = colliderHeight * tr.localScale.x * (1f - stepHeightRatio) * 0.5f;
            float middle = upperLimit + colliderHeight * tr.localScale.x * stepHeightRatio;
            float distanceToGo = middle - distance;

            currentGroundAdjustmentVelocity = tr.up * (distanceToGo / Time.fixedDeltaTime);
        }

        public bool IsGrounded() => isGrounded;
        public Vector3 GetGroundNormal() => sensor.GetNormal();

        // NOTE: Older versions of Unity use rb.velocity instead
        public void SetVelocity(Vector3 velocity) => rb.velocity = velocity + currentGroundAdjustmentVelocity;
        public void SetExtendSensorRange(bool isExtended) => isUsingExtendedSensorRange = isExtended;

        void Setup()
        {
            tr = transform;
            rb = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();

            rb.freezeRotation = true;
            rb.useGravity = false;
        }

        void RecalculateColliderDimensions()
        {
            if (col == null)
            {
                Setup();
            }

            col.height = colliderHeight * (1f - stepHeightRatio);
            col.radius = colliderThickness / 2f;
            col.center = colliderOffset * colliderHeight + new Vector3(0f, stepHeightRatio * col.height / 2f, 0f);

            if (col.height / 2f < col.radius)
            {
                col.radius = col.height / 2f;
            }

            RecalibrateSensor();
        }

        void RecalibrateSensor()
        {
            sensor ??= new RaycastSensor(tr);

            sensor.SetCastOrigin(col.bounds.center - new Vector3(0, col.height / 2, 0));
            sensor.SetCastDirection(RaycastSensor.CastDirection.Down);
            sensor.SetCastRadius(col.radius);
            RecalculateSensorLayerMask();

            const float
                safetyDistanceFactor =
                    0.001f; // Small factor added to prevent clipping issues when the sensor range is calculated

            float length = colliderHeight * (1f - stepHeightRatio) * 0.5f + colliderHeight * stepHeightRatio;
            baseSensorRange = length * (1f + safetyDistanceFactor) * tr.localScale.x;
            // sensor.CastLength = safetyDistanceFactor;
            sensor.CastLength = length * tr.localScale.x;
        }

        void RecalculateSensorLayerMask()
        {
            int objectLayer = gameObject.layer;
            int layerMask = Physics.AllLayers;

            for (int i = 0; i < 32; i++)
            {
                if (Physics.GetIgnoreLayerCollision(objectLayer, i))
                {
                    layerMask &= ~(1 << i);
                }
            }

            int ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
            layerMask &= ~(1 << ignoreRaycastLayer);

            sensor.LayerMask = layerMask;
            currentLayer = objectLayer;
        }

        private void OnDrawGizmos()
        {
            // Sphere();
            // Floor();
            // BoxDraw();
            // DrawCollider();
        }

        private void DrawCollider()
        {
            Gizmos.color = Color.green;
            var radius = col.radius;
            var boxHeight = col.height - radius * 2;
            var basePosition = col.bounds.center;
            var positionModifier = new Vector3(0, col.height / 2 - radius, 0);
            var positionTop = basePosition + positionModifier;
            var positionBottom = basePosition - positionModifier;

            Gizmos.DrawWireCube(basePosition, new Vector3(radius * 2, boxHeight, radius * 2));
            Gizmos.DrawWireSphere(positionTop, radius);
            Gizmos.DrawWireSphere(positionBottom, radius);
        }

        private void BoxDraw()
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            // var width = (float) (col.radius  * Math.PI * 2f);
            var width = col.radius * 2;
            var position = col.bounds.center - new Vector3(0, col.height / 2, 0);
            Gizmos.DrawWireCube(position, new Vector3(width, width, width));
        }

        private void Floor()
        {
            Gizmos.color = Color.red;
            // Gizmos.DrawLine(sensor.GetCastOrigin(), sensor.GetPosition());
        }

        private void Sphere()
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            // var position = col.bounds.center;
            var position = col.bounds.center - new Vector3(0, col.height / 2, 0);
            var width = col.radius;
            // Gizmos.DrawWireSphere(sensor.GetCastOrigin(), sensor.CastLength);
            Gizmos.DrawWireSphere(position, width);
        }
    }
}