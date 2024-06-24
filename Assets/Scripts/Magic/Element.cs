using System;
using System.Collections.Generic;
using UnityEngine;

namespace Magic
{
    public enum ElementType
    {
        Fire,
        Water,
        Air,
        Earth,
        Dark,
        Light
    }

    public class Element
    {
        private ElementType Type { get; }
        public Color Color { get; }

        public Element(ElementType type, Color color)
        {
            Type = type;
            Color = color;
        }

        public static readonly Dictionary<ElementType, Element> Elements = new()
        {
            { ElementType.Fire, new Element(ElementType.Fire, new Color32(200, 35, 35, 255)) },
            { ElementType.Water, new Element(ElementType.Water, new Color32(0, 165, 165, 255)) },
            { ElementType.Air, new Element(ElementType.Air, new Color32(125, 175, 200, 255)) },
            { ElementType.Earth, new Element(ElementType.Earth, new Color32(30, 124, 30, 255)) },
            { ElementType.Dark, new Element(ElementType.Dark, new Color32(0, 0, 0, 255)) },
            { ElementType.Light, new Element(ElementType.Light, new Color32(255, 255, 255, 255)) },
        };

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}