using System;
using Magic;
using UnityEngine;
using UnityEngine.UI;

namespace Spellcasting
{
    public class ElementalReservesUI : MonoBehaviour
    {
        [Range(0, 6)] [SerializeField] private int itemsPerRow = 6;
        [Range(0, 100)] [SerializeField] private int widthBetweenItems = 50;
        [Range(0, 100)] [SerializeField] private int heightBetweenRows = 60;
        [Range(-200, 200)] [SerializeField] private int startingPositionX = 75;
        [Range(-200, 200)] [SerializeField] private int startingPositionY = 10;

        private void Awake()
        {
            var i = 0;
            foreach (var element in Enum.GetValues(typeof(ElementType)))
            {
                var elementalMeter = new GameObject();
                elementalMeter.transform.SetParent(transform);
                elementalMeter.name = $"{element}Reserves";

                var scale = 0.5f;
                var imageScale = new Vector3(scale, scale, scale);
                var meter = (Image)elementalMeter.AddComponent(typeof(Image));
                meter.sprite = Resources.Load<Sprite>($"ElementalMeters/Frames/{element}");
                meter.transform.localScale = imageScale;
                meter.preserveAspect = true;

                var rectTransform = elementalMeter.GetComponent<RectTransform>();
                rectTransform.anchorMin = new Vector2(0.5f, 1);
                rectTransform.anchorMax = new Vector2(0.5f, 1);

                var xPosition = (i % itemsPerRow) * widthBetweenItems + startingPositionX;
                var row = Mathf.FloorToInt(i / (itemsPerRow * 1f));
                var yPosition = -startingPositionY - heightBetweenRows * (row + 1);

                rectTransform.anchoredPosition = new Vector2(xPosition, yPosition);

                var mask = new GameObject();
                mask.name = $"{element}Mask";
                var maskImage = (Image)mask.AddComponent(typeof(Image));
                maskImage.sprite = Resources.Load<Sprite>($"ElementalMeters/Masks/{element}");
                maskImage.transform.localScale = imageScale;
                maskImage.preserveAspect = true;
                maskImage.transform.SetParent(meter.transform);
                var maskImageTransform = maskImage.GetComponent<RectTransform>();
                maskImageTransform.anchoredPosition = Vector2.zero;
                maskImageTransform.anchorMin = new Vector2(0.5f, 0.5f);
                maskImageTransform.anchorMax = new Vector2(0.5f, 0.5f);

                var casing = new GameObject();
                casing.name = $"{element}Case";
                var casingImage = (Image)casing.AddComponent(typeof(Image));
                casingImage.sprite = Resources.Load<Sprite>($"ElementalMeters/Cases/{element}");
                casingImage.transform.localScale = imageScale;
                casingImage.preserveAspect = true;
                casingImage.transform.SetParent(meter.transform);
                var casingImageTransform = casingImage.GetComponent<RectTransform>();
                casingImageTransform.anchoredPosition = Vector2.zero;
                casingImageTransform.anchorMin = new Vector2(0.5f, 0.5f);
                casingImageTransform.anchorMax = new Vector2(0.5f, 0.5f);

                // var fill = new GameObject();// fill.name = $"{element}Fill";
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
}