using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spellcasting
{
    public class Spell
    {
        public string Name { get; }
        public List<KeyCode> Sequence { get; }
        public List<Element> Elements { get; }

        public Spell(string name, List<KeyCode> sequence, List<Element> elements)
        {
            Name = name; 
            Sequence = sequence;
            Elements = elements;
        }

        public override string ToString()
        {
            return $"{Name} [{string.Join(", ", Elements)}]: {string.Join(" -> ", Sequence)}";
        }
    }
}