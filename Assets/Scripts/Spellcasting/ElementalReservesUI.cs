using System;
using System.Collections;
using System.Collections.Generic;
using Spellcasting;
using UnityEngine;
using UnityEngine.UI;

public class ElementalReservesUI : MonoBehaviour
{
    private void Awake()
    {
        int i = 0;
        foreach (var element in Enum.GetValues(typeof(ElementType)))
        {
            var imageObject = new GameObject();
            var image = (Image)imageObject.AddComponent(typeof(Image));
            imageObject.name = $"{element}Reserves";
            image.sprite = Resources.Load<Sprite>($"ElementalMeters/Frames/{element}");
            image.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            image.preserveAspect = true;
            var imageTransform = imageObject.transform;
            imageTransform.SetParent(transform);

            var rectTransform = imageObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);

            var xPosition = (i % 2) * 50 + 50;
            var row = Mathf.FloorToInt(i / 2f);
            var yPosition = -50 * (row + 1);

            rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);


            i++;
        }
    }
}