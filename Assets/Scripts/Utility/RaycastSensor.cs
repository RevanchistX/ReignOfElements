using UnityEngine;

namespace Utility
{
    public class RaycastSensor
    {
        public float CastLength = 1f;
        public LayerMask LayerMask = 255;

        private Vector3 origin = Vector3.zero;
        private readonly Transform transform;

        private CastDirection castDirection;

        public RaycastHit HitInfo;

        public RaycastSensor(Transform playerTransform)
        {
            transform = playerTransform;
        }

        public void Cast()
        {
            var worldOrigin = transform.TransformPoint(origin);
            var worldDirection = GetCastDirection();

            Physics.Raycast(worldOrigin,
                worldDirection,
                out HitInfo,
                CastLength,
                LayerMask,
                QueryTriggerInteraction.Ignore);
        }

        public bool HasDetectedHit() => HitInfo.collider;
        public float GetDistance() => HitInfo.distance;
        public Vector3 GetNormal() => HitInfo.normal;
        public Vector3 GetPosition() => HitInfo.point;
        public Collider GetCollider() => HitInfo.collider;
        public Transform GetTransform() => HitInfo.transform;

        public void SetCastDirection(CastDirection direction) => castDirection = direction;
        public void SetCastOrigin(Vector3 position) => origin = transform.InverseTransformPoint(position);

        private Vector3 GetCastDirection()
        {
            return castDirection switch
            {
                CastDirection.Forward => transform.forward,
                CastDirection.Right => transform.right,
                CastDirection.Up => transform.up,
                CastDirection.Backward => -transform.forward,
                CastDirection.Left => -transform.right,
                CastDirection.Down => -transform.up,
                _ => Vector3.one
            };
        }

        public void DrawDebug()
        {
            if (!HasDetectedHit()) return;

            Debug.DrawRay(HitInfo.point, HitInfo.normal, Color.red, Time.deltaTime);
            const float markerSize = 0.2f;
            Debug.DrawLine(HitInfo.point + Vector3.up * markerSize, HitInfo.point - Vector3.up * markerSize,
                Color.green, Time.deltaTime);
            Debug.DrawLine(HitInfo.point + Vector3.right * markerSize, HitInfo.point - Vector3.right * markerSize,
                Color.green, Time.deltaTime);

            Debug.DrawLine(HitInfo.point + Vector3.forward * markerSize, HitInfo.point - Vector3.forward * markerSize,
                Color.green, Time.deltaTime);
        }
    }
}