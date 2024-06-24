using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Magic
{
    public class ElementSource : MonoBehaviour
    {
        [SerializeField] private ElementType elementType;
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;
        private DateTime duration;
        private Stopwatch stopwatch = new();
        [SerializeField] private GameObject player;

        void Awake()
        {
            _camera = Camera.main;

            if (!Element.Elements.TryGetValue(elementType, out var element)) return;

            //TODO remove when we have assets
            GetComponent<MeshRenderer>().material.SetColor("_Color", element.Color);

            var imageObject = new GameObject();
            imageObject.transform.SetParent(transform);
            imageObject.name = elementType.ToString();

            _spriteRenderer = imageObject.AddComponent<SpriteRenderer>();
            _spriteRenderer.sprite = Resources.Load<Sprite>($"ElementalMeters/Frames/{elementType}");
            _spriteRenderer.enabled = false;
        }


        private void OnMouseEnter()
        {
            _spriteRenderer.enabled = true;
            var scale = 0.2f;
            var spriteRendererTransform = _spriteRenderer.transform;
            spriteRendererTransform.localScale = new Vector3(scale, scale, scale);
            var elementSourcePosition = transform.position;
            spriteRendererTransform.position =
                new Vector3(elementSourcePosition.x, elementSourcePosition.y + 2f, elementSourcePosition.z);
            spriteRendererTransform.LookAt(_camera.transform);
        }

        private void OnMouseExit()
        {
            _spriteRenderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
        }

        private void OnMouseOver()
        {
            var lr = player.GetComponent<LineRenderer>();
            lr.positionCount = 0;
            if (!Input.GetMouseButton(1))
            {
            
                stopwatch.Reset();
                return;
            }

            if (!stopwatch.IsRunning) stopwatch.Start();
            var delta = stopwatch.ElapsedMilliseconds / 1000;

          
            lr.startColor = Color.black;
            lr.endColor = Color.blue;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.positionCount = 2;
            // lr.SetPosition(0, player.transform.position);
            // lr.SetPosition(1, transform.position);
            lr.SetPositions(new[] { player.transform.position, transform.position });
            // Physics.Linecast(player.transform.position, transform.position, out var hitInfo);
            // Debug.Log(lr.GetPosition(0));
        }
    }
}