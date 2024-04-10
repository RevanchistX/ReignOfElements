using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Spellcasting
{
    public class ElementSource : MonoBehaviour
    {
        [SerializeField] private ElementType elementType;
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;

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
    }
}