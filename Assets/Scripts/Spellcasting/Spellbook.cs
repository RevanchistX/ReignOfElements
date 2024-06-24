// using System.Collections.Generic;
// using System.Linq;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using Vector3 = UnityEngine.Vector3;
//
// namespace Spellcasting
// {
//     public class Spellbook : MonoBehaviour
//     {
//         [SerializeField] private Canvas uiCanvas;
//         private List<KeyCode> _inputSequence;
//
//         private Transform _spellbookTransform;
//
//         private readonly List<KeyCode> _allowedKeyCodes = new()
//         {
//             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R
//         };
//
//         private void Awake()
//         {
//             var spellbook = new GameObject();
//             spellbook.name = "Spellbook";
//
//             var imageScale = new Vector3(3, 3, 3);
//             var spellbookImage = (Image)spellbook.AddComponent(typeof(Image));
//             spellbookImage.sprite = Resources.Load<Sprite>("UI/spellbook");
//             spellbookImage.preserveAspect = true;
//             spellbookImage.transform.localScale = imageScale;
//
//             var spellbookRectTransform = spellbook.GetComponent<RectTransform>();
//             spellbookRectTransform.anchorMin = new Vector2(0, 0);
//             spellbookRectTransform.anchorMax = new Vector2(0, 0);
//             spellbookRectTransform.anchoredPosition = new Vector2(150, 120);
//
//             spellbook.transform.SetParent(uiCanvas.transform);
//             _spellbookTransform = spellbook.transform;
//             LoadSpells();
//         }
//
//         private void LoadSpells()
//         {
//             var index = 0;
//
//             foreach (var spell in Spell.SpellList)
//             {
//                 var spellGameObject = new GameObject();
//                 var spellTextComponent = spellGameObject.AddComponent<TextMeshProUGUI>();
//                 spellTextComponent.faceColor = new Color32(0, 0, 0, 255);
//                 spellTextComponent.fontSize = 15;
//                 spellTextComponent.name = spell.Name;
//                 spellTextComponent.text = spell.ToString();
//                 spellTextComponent.autoSizeTextContainer = true;
//                 spellTextComponent.transform.SetParent(_spellbookTransform);
//                 var y = -(25 + index * 7);
//                 var spellPosition = new Vector3(0, y, 0);
//                 spellTextComponent.transform.localPosition = spellPosition;
//                 var rectTransform = spellTextComponent.GetComponent<RectTransform>();
//                 rectTransform.anchorMin = new Vector2(0.5f, 1f);
//                 rectTransform.anchorMax = new Vector2(0.5f, 1f);
//                 index++;
//             }
//         }
//
//         private void FixedUpdate()
//         {
//             var isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
//             uiCanvas.enabled = isCtrlPressed;
//             if (!isCtrlPressed)
//             {
//                 _inputSequence = new List<KeyCode>();
//                 // _sequenceUI.text = "";
//                 return;
//             }
//             if (_inputSequence.Count == 0) return;
//             // _sequenceUI.text = string.Join("", _inputSequence);
//             if (!Spell.SpellList.Select(spell =>
//                         _inputSequence.Count == spell.Sequence.Count && _inputSequence.SequenceEqual(spell.Sequence))
//                     .Any(isSpellCast => isSpellCast)) return;
//             _inputSequence = new List<KeyCode>();
//             uiCanvas.enabled = false;
//         }
//
//
//         private void OnGUI()
//         {
//             var e = Event.current;
//             if (e.type != EventType.KeyDown) return;
//             var isCtrlPressed = e.control;
//             if (!isCtrlPressed)
//             {
//                 _inputSequence = new List<KeyCode>();
//                 return;
//             }
//
//             var currentKeycode = e.keyCode;
//             if (currentKeycode == KeyCode.LeftControl) return;
//             if (!_allowedKeyCodes.Contains(currentKeycode)) return;
//             _inputSequence.Add(currentKeycode);
//         }
//     }
//     // [SerializeField] private Canvas uiCanvas;
//
//     // private List<GameObject> pillars = new();
//
//
//     //     private Camera _camera;
//     //
//     //  
//     //
//
//     //     private MeshRenderer _meshRenderer;
//     //     private TextMeshProUGUI _sequenceUI;
//     //     private LineRenderer _lineRenderer;
//     //
//     //     private void Awake()
//     //     {
//     //         _lineRenderer = GetComponent<LineRenderer>();
//     //
//     //         _camera = Camera.main;
//     //         _meshRenderer = GetComponent<MeshRenderer>();
//     //         var fontColor = new Color32(255, 255, 255, 255);
//     //
//     //         var spellbookGameObject = new GameObject();
//     //         var spellbookTextComponent = spellbookGameObject.AddComponent<TextMeshProUGUI>();
//     //         spellbookTextComponent.name = "Spellbook";
//     //         spellbookTextComponent.faceColor = fontColor;
//     //         spellbookTextComponent.fontSize = 20;
//     //         spellbookTextComponent.text = "Spellbook";
//     //         spellbookTextComponent.autoSizeTextContainer = true;
//     //
//     //         var canvasTransform = uiCanvas.transform;
//     //         spellbookTextComponent.transform.SetParent(canvasTransform);
//     //         var canvasRectTransform = uiCanvas.GetComponent<RectTransform>();
//     //         var sizeDelta = canvasRectTransform.sizeDelta;
//     //         var position = canvasTransform.position;
//     //         var spellbookPosition = position + new Vector3(-sizeDelta.x / 2 + 110, sizeDelta.y / 2 - 50, position.z);
//     //         spellbookTextComponent.transform.position = spellbookPosition;
//     //
//     //         var index = 0;
//     //         // foreach (var spell in spellbook)
//     //         // {
//     //         //     var spellGameObject = new GameObject();
//     //         //     var spellTextComponent = spellGameObject.AddComponent<TextMeshProUGUI>();
//     //         //     spellTextComponent.faceColor = fontColor;
//     //         //     spellTextComponent.fontSize = 15;
//     //         //     spellTextComponent.name = spell.Name;
//     //         //     spellTextComponent.text = spell.ToString();
//     //         //     spellTextComponent.autoSizeTextContainer = true;
//     //         //     spellTextComponent.transform.SetParent(spellbookTextComponent.transform);
//     //         //     var y = -(50 + index * 25);
//     //         //     var spellPosition = spellbookPosition + new Vector3(0, y, 0);
//     //         //     spellTextComponent.transform.position = spellPosition;
//     //         //     index++;
//     //         // }
//     //
//     //         var sequence = new GameObject();
//     //         _sequenceUI = sequence.AddComponent<TextMeshProUGUI>();
//     //         _sequenceUI.faceColor = fontColor;
//     //         _sequenceUI.fontSize = 15;
//     //         _sequenceUI.name = "Sequence";
//     //         _sequenceUI.autoSizeTextContainer = true;
//     //         _sequenceUI.transform.SetParent(uiCanvas.transform);
//     //         _sequenceUI.transform.position = position + new Vector3(0, sizeDelta.y / 2, 0);
//     //
//     //
//     //         InitElements();
//     //     }
//     //
//     //     private void InitElements()
//     //     {
//     //         // var radius = 3;
//     //         // int i = 0;
//     //         // foreach (var element in Element.Elements)
//     //         // {
//     //         //     var angle = i * Mathf.PI * 2f / Element.Elements.Count;
//     //         //     var x = radius * Mathf.Cos(angle);
//     //         //     var z = radius * Mathf.Sin(angle);
//     //         //     var elementPillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
//     //         //     elementPillar.transform.position = new Vector3(x, 1, z);
//     //         //     // elementPillar.name = $"Pillar_{element.Name}";
//     //         //     // elementPillar.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", element.Color);
//     //         //     pillars.Add(elementPillar);
//     //         //     i++;
//     //         // }
//     //     }
//     // }
// }