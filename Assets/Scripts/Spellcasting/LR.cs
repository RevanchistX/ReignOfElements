using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Spellcasting;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LR : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;

    private List<Vector3> points = new();

    private Camera camera;

    private GameObject target;
    [SerializeField] private Material material;
    [SerializeField] private Mesh mesh;
    [SerializeField] private GameObject parent;

    void Start()
    {
        lr.positionCount = 0;
        // lr.startColor = Color.white;
        // lr.endColor = Color.red;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.useWorldSpace = true;
        lr.material = material;
        camera = Camera.main;
        // Debug.Log(mesh);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var worldPosition =
                camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));

            if (points.Contains(worldPosition)) return;
            Debug.Log($"wp: {worldPosition}\nmp: {Input.mousePosition}");
            points.Add(worldPosition);
            lr.positionCount = points.Count;
            lr.SetPosition(lr.positionCount - 1, worldPosition);
        }
        else
        {
            SpawnShape();
            lr.positionCount = 0;
            points.Clear();
        }
    }

    private void SpawnShape()
    {
        if (points.Count == 0) return;
        CreateMesh("basic");
        // CreateMesh("transform", true);
    }

    private void CreateMesh(string name)
    {
        var pointsToUse = points.Select(p => p - transform.position);

        var transformationsMesh = new Mesh
        {
            name = $"{name} Mesh",
            vertices = points.ToArray(),
        };
        var gameObj = new GameObject
        {
            name = $"{name}"
        };
        var meshRenderer = gameObj.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        var meshFilter = gameObj.AddComponent<MeshFilter>();
        meshFilter.mesh = transformationsMesh;

        var bc = gameObj.AddComponent<BoxCollider>();
        bc.isTrigger = true;

        // gameObj.AddComponent<Rigidbody>();
        gameObj.AddComponent<ColiderDetector>();
        lr.BakeMesh(transformationsMesh);
        // gameObj.transform.position = mesh.bounds.center;
        gameObj.transform.position = transform.position;
    }
}