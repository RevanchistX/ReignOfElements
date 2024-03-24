using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    [SerializeField] private Canvas uiCanvas;

    private Dictionary<string, List<KeyCode>> elements = new()
    {
        { "FIRE", new List<KeyCode> { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R } },
        { "WATER", new List<KeyCode> { KeyCode.Q, KeyCode.W, KeyCode.R, KeyCode.E } },
        { "AIR", new List<KeyCode> { KeyCode.Q, KeyCode.E, KeyCode.W, KeyCode.R } },
        { "EARTH", new List<KeyCode> { KeyCode.Q, KeyCode.E, KeyCode.R, KeyCode.W } },
        { "DARKNESS", new List<KeyCode> { KeyCode.Q, KeyCode.R, KeyCode.E, KeyCode.W } },
        { "LIGHT", new List<KeyCode> { KeyCode.Q, KeyCode.R, KeyCode.W, KeyCode.E } },
        { "METAL", new List<KeyCode> { KeyCode.R, KeyCode.Q, KeyCode.E, KeyCode.W } },
        { "MIND", new List<KeyCode> { KeyCode.R, KeyCode.Q, KeyCode.W, KeyCode.E } }
    };

    private Dictionary<string, Color> colors = new()
    {
        { "FIRE", new Color32(200, 35, 35, 255) },
        { "WATER", new Color32(0, 165, 165, 255) },
        { "AIR", new Color32(125, 175, 200, 255) },
        { "EARTH", new Color32(30, 124, 30, 255) },
        { "DARKNESS", new Color32(0, 0, 0, 255) },
        { "LIGHT", new Color32(255, 255, 255, 255) },
        { "METAL", new Color32(180, 180, 180, 255) },
        { "MIND", new Color32(125, 30, 115, 255) }
    };


    private readonly List<KeyCode> _allowedKeyCodes = new()
    {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R
    };

    private List<KeyCode> _inputSequence = new();
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        var fontColor = new Color32(255, 0, 0, 100);
        // var backgroundColor = new Color32(255, 0, 0, 100);
        var spellbook = new GameObject();
        var spellbookTextComponent = spellbook.AddComponent<TextMeshProUGUI>();
        spellbookTextComponent.name = "Spellbook";
        spellbookTextComponent.faceColor = fontColor;
        spellbookTextComponent.fontSize = 20;
        spellbookTextComponent.text = "Spellbook";
        spellbookTextComponent.autoSizeTextContainer = true;
        spellbookTextComponent.alignment = TextAlignmentOptions.Right;

        var canvasTransform = uiCanvas.transform;
        spellbookTextComponent.transform.SetParent(canvasTransform);
        var canvasRectTransform = uiCanvas.GetComponent<RectTransform>();
        var sizeDelta = canvasRectTransform.sizeDelta;
        var position = canvasTransform.position;
        var spellbookPosition = position + new Vector3(-sizeDelta.x / 2 + 110, sizeDelta.y / 2 - 50, position.z);
        spellbookTextComponent.transform.position = spellbookPosition;
        Debug.Log(spellbookPosition);
        var index = 0;
        foreach (var element in elements)
        {
            var spell = new GameObject();
            var spellTextComponent = spell.AddComponent<TextMeshProUGUI>();
            spellTextComponent.faceColor = fontColor;
            spellTextComponent.fontSize = 15;
            spellTextComponent.name = element.Key;
            spellTextComponent.text = element.Key + ": " + string.Join(" -> ", element.Value);
            spellTextComponent.autoSizeTextContainer = true;
            spellTextComponent.alignment = TextAlignmentOptions.Right;
            spellTextComponent.transform.SetParent(spellbookTextComponent.transform);
            var y = -(50 + index * 25);
            var spellPosition = spellbookPosition + new Vector3(0, y, 0);
            spellTextComponent.transform.position = spellPosition;
            index++;
        }
    }

    private void FixedUpdate()
    {
        var isCtrlPressed = Input.GetKey(KeyCode.LeftControl);
        uiCanvas.enabled = isCtrlPressed;
        if (!isCtrlPressed)
        {
            _inputSequence = new List<KeyCode>();
            return;
        }

        if (_inputSequence.Count == 0) return;
        foreach (var kvPair in elements)
        {
            var elementSequence = kvPair.Value;
            if (elementSequence.Count != _inputSequence.Count) continue;
            if (!elementSequence.SequenceEqual(_inputSequence))
            {
                continue;
            }

            if (colors.TryGetValue(kvPair.Key, out var elementColor))
            {
                _meshRenderer.materials[0].SetColor("_Color", elementColor);
            }

            Debug.Log("element: " + kvPair.Key);
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