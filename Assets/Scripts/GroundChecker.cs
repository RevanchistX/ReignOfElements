using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField]
    private float groundDistance = 0.08f;

    [SerializeField]
    private LayerMask groundLayer;

    // [SerializeField]
    // private bool drawGizmos;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        Debug.Log($"is grounded {IsGrounded}");
    }

    // private void Update()
    // {
    //     // IsGrounded = Physics.BoxCast(transform.position, Vector3.one / 2, Vector3.down, out var hitInfo, Quaternion.Euler(Vector3.zero),
    //     //     groundDistance, groundLayer);
    //     // Debug.Log($"hit info: {hitInfo.transform.gameObject}");
    //     IsGrounded = Physics.SphereCast(transform.position,
    //         groundDistance,
    //         Vector3.down,
    //         out var hitInfo,
    //         groundDistance,
    //         groundLayer);
    //     if (IsGrounded)
    //     {
    //         Debug.Log($"hitInfo {hitInfo.transform.gameObject}");
    //     }
    // }

    // private void OnDrawGizmos()
    // {
    //     if (!drawGizmos) return;
    //     Gizmos.color = Color.red;
    //     // Gizmos.DrawCube(transform.position, Vector3.one / 2);
    //     Gizmos.DrawSphere(transform.position, groundDistance);
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Floor")) return;
        IsGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.transform.CompareTag("Floor")) return;
        IsGrounded = false;
    }
}