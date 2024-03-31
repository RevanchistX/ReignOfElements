using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Spellcasting
{
    public class Spellbook : MonoBehaviour
    {
        [SerializeField] private Canvas uiCanvas;

        private List<GameObject> pillars = new();

        private List<Spell> spellbook = new()
        {
            new Spell("Ignis",
                new List<KeyCode> { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R },
                new List<Element> { Element.FIRE }
            ),
            new Spell("Aquas",
                new List<KeyCode> { KeyCode.R, KeyCode.E, KeyCode.W, KeyCode.Q },
                new List<Element> { Element.WATER }
            ),
            new Spell("Æerth",
                new List<KeyCode> { KeyCode.R, KeyCode.R, KeyCode.W, KeyCode.W },
                new List<Element> { Element.EARTH }
            ),
            new Spell("Æer",
                new List<KeyCode> { KeyCode.W, KeyCode.W, KeyCode.R, KeyCode.R },
                new List<Element> { Element.AIR }
            ),
            new Spell("Umbara",
                new List<KeyCode> { KeyCode.Q, KeyCode.Q, KeyCode.Q, KeyCode.E },
                new List<Element> { Element.DARKNESS }
            ),
            new Spell("Lux",
                new List<KeyCode> { KeyCode.E, KeyCode.E, KeyCode.E, KeyCode.Q },
                new List<Element> { Element.LIGHT }
            ),
            new Spell("Mætal",
                new List<KeyCode> { KeyCode.W, KeyCode.R, KeyCode.W, KeyCode.R },
                new List<Element> { Element.METAL }
            ),
            new Spell("Mynd",
                new List<KeyCode> { KeyCode.R, KeyCode.W, KeyCode.R, KeyCode.W },
                new List<Element> { Element.MIND }
            ),
        };


        private Camera _camera;

        private readonly List<KeyCode> _allowedKeyCodes = new()
        {
            KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R
        };

        private List<KeyCode> _inputSequence;
        private MeshRenderer _meshRenderer;
        private TextMeshProUGUI _sequenceUI;

        private void Awake()
        {
            _camera = (Camera)FindObjectOfType(typeof(Camera));
            _camera.transform.position = gameObject.transform.position;
            _camera.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            // var mouseX = Input.GetAxis("Mouse X");
            //
            // var rotation = transform.eulerAngles;
            // transform.eulerAngles = new Vector3(rotation.x, rotation.y + mouseX, rotation.z);
            // _camera.transform.LookAt(transform);
            //
            _meshRenderer = GetComponent<MeshRenderer>();
            var fontColor = new Color32(255, 255, 255, 255);

            var spellbookGameObject = new GameObject();
            var spellbookTextComponent = spellbookGameObject.AddComponent<TextMeshProUGUI>();
            spellbookTextComponent.name = "Spellbook";
            spellbookTextComponent.faceColor = fontColor;
            spellbookTextComponent.fontSize = 20;
            spellbookTextComponent.text = "Spellbook";
            spellbookTextComponent.autoSizeTextContainer = true;

            var canvasTransform = uiCanvas.transform;
            spellbookTextComponent.transform.SetParent(canvasTransform);
            var canvasRectTransform = uiCanvas.GetComponent<RectTransform>();
            var sizeDelta = canvasRectTransform.sizeDelta;
            var position = canvasTransform.position;
            var spellbookPosition = position + new Vector3(-sizeDelta.x / 2 + 110, sizeDelta.y / 2 - 50, position.z);
            spellbookTextComponent.transform.position = spellbookPosition;

            var index = 0;
            foreach (var spell in spellbook)
            {
                var spellGameObject = new GameObject();
                var spellTextComponent = spellGameObject.AddComponent<TextMeshProUGUI>();
                spellTextComponent.faceColor = fontColor;
                spellTextComponent.fontSize = 15;
                spellTextComponent.name = spell.Name;
                spellTextComponent.text = spell.ToString();
                spellTextComponent.autoSizeTextContainer = true;
                spellTextComponent.transform.SetParent(spellbookTextComponent.transform);
                var y = -(50 + index * 25);
                var spellPosition = spellbookPosition + new Vector3(0, y, 0);
                spellTextComponent.transform.position = spellPosition;
                index++;
            }

            var sequence = new GameObject();
            _sequenceUI = sequence.AddComponent<TextMeshProUGUI>();
            _sequenceUI.faceColor = fontColor;
            _sequenceUI.fontSize = 15;
            _sequenceUI.name = "Sequence";
            _sequenceUI.autoSizeTextContainer = true;
            _sequenceUI.transform.SetParent(uiCanvas.transform);
            _sequenceUI.transform.position = position + new Vector3(0, sizeDelta.y / 2, 0);


            InitElements();
        }

        private void InitElements()
        {
            var radius = 5;
            int i = 0;
            foreach (var element in Element.ELEMENTS)
            {
                var angle = i * Mathf.PI * 2f / Element.ELEMENTS.Count;
                var x = radius * Mathf.Cos(angle);
                var z = radius * Mathf.Sin(angle);
                var elementPillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                elementPillar.transform.position = new Vector3(x, 1, z);
                elementPillar.name = $"Pillar_{element.Name}";
                elementPillar.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", element.Color);
                pillars.Add(elementPillar);
                i++;
            }
        }

        private void FixedUpdate()
        {
            // var rotation = Vector2.zero;
            // rotation.x += Input.GetAxis("Mouse X") * 2f;
            // rotation.y += Input.GetAxis("Mouse Y") * 2f;
            // rotation.y = Mathf.Clamp(rotation.y, -88f, 88f);
            // var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            // var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);
            //
            // transform.localRotation = xQuat * yQuat; //Quaternion
            // _camera.transform.LookAt(transform);
            // // _camera.transform.rotation = gameObject.transform.rotation;
            // var position = Input.mousePosition;
            // position.z = 10f;
            // // position.z = _camera.nearClipPlane + 1;
            // var point = _camera.ScreenToWorldPoint(position) * 100;
            // point.y = 1;
            //
            // Debug.DrawLine(transform.position, point, Color.red);
            // var isRMBDown = Input.GetMouseButtonDown(1);
            // var isRMBUp = Input.GetMouseButtonUp(1);
            // var stopwatch = new Stopwatch();
            // if (isRMBDown)
            // {
            //     stopwatch.Start();
            //     Debug.Log("i am pressed");
            //     if (Physics.Raycast(transform.position, point, out var hitInfo))
            //     {
            //         Debug.Log("i am hit");
            //         Debug.Log(hitInfo.transform.gameObject.name);
            //         var pillar = hitInfo.transform.gameObject;
            //         _camera.transform.LookAt(pillar.transform);
            //         // _meshRenderer.materials[0]
            //         //     .SetColor("_Color", pillar.GetComponent<MeshRenderer>().materials[0].color);
            //         // return;
            //     }
            //     stopwatch.Stop();
            //     Debug.Log(stopwatch.ElapsedMilliseconds);
            //     return;
            //     // Debug.DrawLine(Vector3.zero, Vector3.up * 100, Color.red);
            // }

            var isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
            uiCanvas.enabled = isCtrlPressed;
            if (!isCtrlPressed)
            {
                _inputSequence = new List<KeyCode>();
                _sequenceUI.text = "";
                return;
            }

            if (_inputSequence.Count == 0) return;
            _sequenceUI.text = string.Join("", _inputSequence);


            foreach (var spell in spellbook)
            {
                var isSpellCast = _inputSequence.Count == spell.Sequence.Count &&
                                  _inputSequence.SequenceEqual(spell.Sequence);
                if (!isSpellCast) continue;
                _meshRenderer.materials[0].SetColor("_Color", spell.Elements[0].Color);
                _inputSequence = new List<KeyCode>();
                uiCanvas.enabled = false;
                break;
            }
        }

        private void OnGUI()
        {
            var e = Event.current;
            if (e.type != EventType.KeyDown) return;
            var isCtrlPressed = e.control;
            if (!isCtrlPressed)
            {
                _inputSequence = new List<KeyCode>();
                return;
            }

            var currentKeycode = e.keyCode;
            if (currentKeycode == KeyCode.LeftControl) return;
            if (!_allowedKeyCodes.Contains(currentKeycode)) return;
            _inputSequence.Add(currentKeycode);
        }
    }
}