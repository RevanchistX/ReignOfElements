using UnityEngine;
using Utility;

namespace Controllers.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class Mover : MonoBehaviour
    {
        #region Fields

        [Header("Collider Settings:")]
        [Range(0f, 1f)]
        [SerializeField]
        private float stepHeightRatio = 0.1f;

        [SerializeField]
        private float colliderHeight = 2f;

        [SerializeField]
        private float colliderThickness = 1f;

        [SerializeField]
        private Vector3 colliderOffset = Vector3.zero;

        private Rigidbody rigidBody;
        private CapsuleCollider colliderReference;
        private RaycastSensor sensor;

        private bool isGrounded;

        private float baseSensorRange;

        // Velocity to adjust player position to maintain ground contact
        private Vector3 currentGroundAdjustmentVelocity;

        private int currentLayer;

        [Header("Sensor Settings:")]
        [SerializeField]
        private bool isInDebugMode;

        private bool isUsingExtendedSensorRange = true; // Use extended range for smoother ground transitions

        #endregion

        private void Awake()
        {
            Setup();
            RecalculateColliderDimensions();
        }

        private void OnValidate()
        {
            if (gameObject.activeInHierarchy)
            {
                RecalculateColliderDimensions();
            }
        }

        private void LateUpdate()
        {
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
                ? baseSensorRange + colliderHeight * transform.localScale.x * stepHeightRatio
                : baseSensorRange;
            sensor.Cast();

            isGrounded = sensor.HasDetectedHit();
            if (!isGrounded) return;

            var distance = sensor.GetDistance();
            var upperLimit = colliderHeight * transform.localScale.x * (1f - stepHeightRatio) * 0.5f;
            var middle = upperLimit + colliderHeight * transform.localScale.x * stepHeightRatio;
            var distanceToGo = middle - distance;

            currentGroundAdjustmentVelocity = transform.up * (distanceToGo / Time.fixedDeltaTime);
        }

        public bool IsGrounded() => isGrounded;
        public Vector3 GetGroundNormal() => sensor.GetNormal();

        public void SetVelocity(Vector3 velocity) => rigidBody.velocity = velocity + currentGroundAdjustmentVelocity;
        public void SetExtendSensorRange(bool isExtended) => isUsingExtendedSensorRange = isExtended;

        private void Setup()
        {
            rigidBody = GetComponent<Rigidbody>();
            colliderReference = GetComponent<CapsuleCollider>();

            rigidBody.freezeRotation = true;
            rigidBody.useGravity = false;
        }

        private void RecalculateColliderDimensions()
        {
            if (colliderReference == null)
            {
                Setup();
            }

            colliderReference.height = colliderHeight * (1f - stepHeightRatio);
            colliderReference.radius = colliderThickness / 2f;
            colliderReference.center = colliderOffset * colliderHeight +
                                       new Vector3(0f, stepHeightRatio * colliderReference.height / 2f, 0f);

            if (colliderReference.height / 2f < colliderReference.radius)
            {
                colliderReference.radius = colliderReference.height / 2f;
            }

            RecalibrateSensor();
        }

        private void RecalibrateSensor()
        {
            sensor ??= new RaycastSensor(transform);

            sensor.SetCastOrigin(colliderReference.bounds.center);
            sensor.SetCastDirection(CastDirection.Down);
            RecalculateSensorLayerMask();

            // Small factor added to prevent clipping issues when the sensor range is calculated
            const float safetyDistanceFactor = 0.001f;

            var length = colliderHeight * (1f - stepHeightRatio) * 0.5f + colliderHeight * stepHeightRatio;
            baseSensorRange = length * (1f + safetyDistanceFactor) * transform.localScale.x;
            sensor.CastLength = length * transform.localScale.x;
        }

        private void RecalculateSensorLayerMask()
        {
            var objectLayer = gameObject.layer;
            var layerMask = Physics.AllLayers;

            for (var i = 0; i < 32; i++)
            {
                if (Physics.GetIgnoreLayerCollision(objectLayer, i))
                {
                    layerMask &= ~(1 << i);
                }
            }

            var ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
            layerMask &= ~(1 << ignoreRaycastLayer);

            sensor.LayerMask = layerMask;
            currentLayer = objectLayer;
        }
    }
}