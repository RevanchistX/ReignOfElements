using System.Collections.Generic;
using UnityEngine;

namespace Spellcasting
{
    public class Element
    {
        public string Name { get; }
        public Color Color { get; }

        public Element(string name, Color color)
        {
            Name = name;
            Color = color;
        }


        public static readonly Element FIRE = new("Fire", new Color32(200, 35, 35, 255));
        public static readonly Element WATER = new("Water", new Color32(0, 165, 165, 255));
        public static readonly Element AIR = new("Air", new Color32(125, 175, 200, 255));
        public static readonly Element EARTH = new("Earth", new Color32(30, 124, 30, 255));
        public static readonly Element DARKNESS = new("Darkness", new Color32(0, 0, 0, 255));
        public static readonly Element LIGHT = new("Light", new Color32(255, 255, 255, 255));
        public static readonly Element METAL = new("Metal", new Color32(180, 180, 180, 255));
        public static readonly Element MIND = new("Mind", new Color32(125, 30, 115, 255));

        public static readonly List<Element> ELEMENTS = new()
        {
            FIRE,
            WATER,
            AIR,
            EARTH,
            DARKNESS,
            LIGHT,
            METAL,
            MIND
        };
        
        
        public override string ToString()
        {
            return Name;
        }
    }
}