// using System.Collections.Generic;
// using UnityEngine;
//
// namespace Spellcasting
// {
//     public class Flow : MonoBehaviour
//     {
//         private List<Element> _elementalReserves;
//         private Camera _camera;
//
//         private readonly List<KeyCode> _allowedKeyCodes = new()
//         {
//             KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R
//         };
//
//         void Awake()
//         {
//             _camera = Camera.main;
//         }
//
//         private void FixedUpdate()
//         {
//             // acquire elements
//             // consume elements to cast spell
//             // draw spell shape
//         }
//
//         // void CastSpell()
//         // {
//         // var isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
//         // uiCanvas.enabled = isCtrlPressed;
//         // if (!isCtrlPressed)
//         // {
//         //     _inputSequence = new List<KeyCode>();
//         //     _sequenceUI.text = "";
//         //     return;
//         // }
//         //
//         // if (_inputSequence.Count == 0) return;
//         // _sequenceUI.text = string.Join("", _inputSequence);
//         //
//         //
//         // foreach (var spell in spellbook)
//         // {
//         //     var isSpellCast = _inputSequence.Count == spell.Sequence.Count &&
//         //                       _inputSequence.SequenceEqual(spell.Sequence);
//         //     if (!isSpellCast) continue;
//         //     _meshRenderer.materials[0].SetColor("_Color", spell.Elements[0].Color);
//         //     _inputSequence = new List<KeyCode>();
//         //     uiCanvas.enabled = false;
//         //     break;
//         // }
//         // }
//
//
//         // private void OnGUI()
//         // {
//         // var e = Event.current;
//         // if (e.type != EventType.KeyDown) return;
//         // var isCtrlPressed = e.control;
//         // if (!isCtrlPressed)
//         // {
//         //     _inputSequence = new List<KeyCode>();
//         //     return;
//         // }
//         //
//         // var currentKeycode = e.keyCode;
//         // if (currentKeycode == KeyCode.LeftControl) return;
//         // if (!_allowedKeyCodes.Contains(currentKeycode)) return;
//         // _inputSequence.Add(currentKeycode);
//         // }
//
//
//         // void ConsumeElement()
//         // {
//         //     var isRmbDown = Input.GetMouseButtonDown(1);
//         //     if (!isRmbDown) return;
//         //     var ray = _camera.ScreenPointToRay(Input.mousePosition);
//         //     ray.direction *= 100;
//         //     if (!Physics.Raycast(ray, out var hit)) return;
//         //     var pillar = hit.transform.gameObject;
//         //     var material = pillar.GetComponent<MeshRenderer>().material;
//         //     foreach (Transform child in transform)
//         //     {
//         //         child.gameObject.GetComponent<MeshRenderer>().material = material;
//         //     }
//         //     Destroy(pillar);
//         // }
//     }
// }