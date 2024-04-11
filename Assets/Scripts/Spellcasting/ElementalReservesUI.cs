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
            var elementalMeter = new GameObject();
            elementalMeter.name = $"{element}Reserves";
            elementalMeter.transform.SetParent(transform);

            var imageScale = new Vector3(0.4f, 0.4f, 0.4f);
            var meter = (Image)elementalMeter.AddComponent(typeof(Image));
            meter.sprite = Resources.Load<Sprite>($"ElementalMeters/Frames/{element}Frame");
            meter.transform.localScale = imageScale;
            meter.preserveAspect = true;

            var rectTransform = elementalMeter.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);

            const float numberOfRows = 4f;
            const int widthBetweenItems = 50;
            const int heightBetweenRows = 60;
            const int startingPositionX = 75;
            const int startingPositionY = 10;
            var xPosition = (i % numberOfRows) * widthBetweenItems + startingPositionX;
            var row = Mathf.FloorToInt(i / numberOfRows);
            var yPosition = -startingPositionY - heightBetweenRows * (row + 1);

            rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

            var nestedLocation = new Vector3(50, -50, 1);
            
            var mask = new GameObject();
            mask.name = $"{element}Mask";
            var maskImage = (Image)mask.AddComponent(typeof(Image));
            maskImage.sprite = Resources.Load<Sprite>($"ElementalMeters/Masks/{element}Mask");
            maskImage.transform.localScale = imageScale;
            maskImage.preserveAspect = true;
            maskImage.transform.SetParent(meter.transform);
            var maskImageTransform = maskImage.GetComponent<RectTransform>();
            maskImageTransform.anchoredPosition = nestedLocation;
            maskImageTransform.anchorMin = rectTransform.anchorMin;
            maskImageTransform.anchorMax = rectTransform.anchorMax;

            var casing = new GameObject();
            casing.name = $"{element}Case";
            var casingImage = (Image)casing.AddComponent(typeof(Image));
            casingImage.sprite = Resources.Load<Sprite>($"ElementalMeters/Cases/{element}Case8Section");
            print($"ElementalMeters/Cases/{element}Case8Sections");
            casingImage.transform.localScale = imageScale;
            casingImage.preserveAspect = true;
            casingImage.transform.SetParent(meter.transform);
            var casingImageTransform = casingImage.GetComponent<RectTransform>();
            casingImageTransform.anchoredPosition = nestedLocation;
            casingImageTransform.anchorMin = rectTransform.anchorMin;
            casingImageTransform.anchorMax = rectTransform.anchorMax;
            
            // var fill = new GameObject();
            // fill.name = $"{element}Fill";
            // var fillImage = (Image)fill.AddComponent(typeof(Image));
            // fillImage.sprite = Resources.Load<Sprite>($"ElementalMeters/Fill/BlankFillHexagonal");
            // fillImage.transform.localScale = imageScale;
            // fillImage.preserveAspect = true;
            // fillImage.transform.SetParent(maskImage.transform);
            // var fillImageTransform = fillImage.GetComponent<RectTransform>();
            // fillImageTransform.anchoredPosition = rectTransform.anchoredPosition;
            // fillImageTransform.anchorMin = rectTransform.anchorMin;
            // fillImageTransform.anchorMax = rectTransform.anchorMax;


            i++;
        }
    }
}