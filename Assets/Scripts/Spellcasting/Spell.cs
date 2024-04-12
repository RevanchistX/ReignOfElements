using System.Collections.Generic;
using UnityEngine;

namespace Spellcasting
{
    public class Spell
    {
        public string Name { get; }
        public List<KeyCode> Sequence { get; }
        public List<ElementType> Elements { get; }

        public Spell(string name, List<KeyCode> sequence, List<ElementType> elements)
        {
            Name = name;
            Sequence = sequence;
            Elements = elements;
        }

        public override string ToString()
        {
            return $"{Name} [{string.Join(", ", Elements)}]: {string.Join(" -> ", Sequence)}";
        }


        public static readonly List<Spell> SpellList = new()
        {
            new Spell("Ignis",
                new List<KeyCode> { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R },
                new List<ElementType> { ElementType.Fire }
            ),
            new Spell("Aquas",
                new List<KeyCode> { KeyCode.R, KeyCode.E, KeyCode.W, KeyCode.Q },
                new List<ElementType> { ElementType.Water }
            ),
            new Spell("Æerth",
                new List<KeyCode> { KeyCode.R, KeyCode.R, KeyCode.W, KeyCode.W },
                new List<ElementType> { ElementType.Earth }
            ),
            new Spell("Æer",
                new List<KeyCode> { KeyCode.W, KeyCode.W, KeyCode.R, KeyCode.R },
                new List<ElementType> { ElementType.Air }
            ),
            new Spell("Umbara",
                new List<KeyCode> { KeyCode.Q, KeyCode.Q, KeyCode.Q, KeyCode.E },
                new List<ElementType> { ElementType.Darkness }
            ),
            new Spell("Lux",
                new List<KeyCode> { KeyCode.E, KeyCode.E, KeyCode.E, KeyCode.Q },
                new List<ElementType> { ElementType.Light }
            ),
            new Spell("Mætal",
                new List<KeyCode> { KeyCode.W, KeyCode.R, KeyCode.W, KeyCode.R },
                new List<ElementType> { ElementType.Metal }
            ),
            new Spell("Mynd",
                new List<KeyCode> { KeyCode.R, KeyCode.W, KeyCode.R, KeyCode.W },
                new List<ElementType> { ElementType.Mind }
            ),
        };
    }
}