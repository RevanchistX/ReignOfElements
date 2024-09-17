using SimpleMan.VisualRaycast;
using UnityEngine;

namespace Prototype
{
    public class RaycastSensor
    {
        public float CastLength = 1f;
        public LayerMask LayerMask = 255;

        private Vector3 origin = Vector3.zero;
        private readonly Transform transform;

        public enum CastDirection
        {
            Forward,
            Right,
            Up,
            Backward,
            Left,
            Down
        }

        private CastDirection castDirection;

        private RaycastHit hitInfo;

        public RaycastSensor(Transform playerTransform)
        {
            transform = playerTransform;
        }

        public void Cast()
        {
            var worldOrigin = transform.TransformPoint(origin);
            var worldDirection = GetCastDirection();
            hitInfo = ComponentExtension.MakeBoxcast(transform, new Ray(worldOrigin, worldDirection), 0.1f,
                Vector3.one * radius, Quaternion.identity, LayerMask, false, true).FirstHit;

            // RaycastHit[] hits = Physics.BoxCastAll(worldOrigin, Vector3.one * radius, worldDirection, Quaternion.identity, float.MaxValue, LayerMask);
            // CastResult result = ComponentExtension.CalculateResult(hits, transform, true, true);
            // VisualCastDrawer.Instance?.
            // Debug.Log($"wo {worldOrigin}, radius {radius}, wd {worldDirection}, cl {CastLength}");
            // Physics.BoxCast(worldOrigin, Vector3.one * (radius * 2), worldDirection, out hitInfo, Quaternion.identity, CastLength, LayerMask, QueryTriggerInteraction.Ignore);
            // Physics.SphereCast(worldOrigin, radius, worldDirection, out hitInfo, radius, LayerMask,
            // QueryTriggerInteraction.Ignore);
            // Physics.Raycast(worldOrigin, worldDirection, out hitInfo, CastLength, LayerMask,
            //     QueryTriggerInteraction.Ignore);
        }

        public bool HasDetectedHit() => hitInfo.collider;
        public float GetDistance() => hitInfo.distance;
        public Vector3 GetNormal() => hitInfo.normal;
        public Vector3 GetPosition() => hitInfo.point;
        public Collider GetCollider() => hitInfo.collider;
        public Transform GetTransform() => hitInfo.transform;

        public void SetCastDirection(CastDirection direction) => castDirection = direction;
        public void SetCastOrigin(Vector3 position) => origin = transform.InverseTransformPoint(position);
        private float radius;
        public void SetCastRadius(float radius) => this.radius = radius;

        public Vector3 GetCastOrigin() => origin;

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

            Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red, Time.deltaTime);
            const float markerSize = 0.2f;
            // Debug.DrawLine(hitInfo.point + Vector3.up * markerSize, hitInfo.point - Vector3.up * markerSize,
            //     Color.green, Time.deltaTime);
            Debug.DrawLine(hitInfo.point + Vector3.right * markerSize, hitInfo.point - Vector3.right * markerSize,
                Color.green, Time.deltaTime);
            Debug.DrawLine(hitInfo.point + Vector3.forward * markerSize, hitInfo.point - Vector3.forward * markerSize,
                Color.green, Time.deltaTime);
        }
    }
}