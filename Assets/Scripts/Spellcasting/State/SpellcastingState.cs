using UnityEngine;

namespace Spellcasting
{
    public class SpellcastingState
    {
        public virtual bool IsCasting()
        {
            return false;
        }

        public virtual bool IsHarvesting()
        {
            return false;
        }

        public virtual float GetMovementModifier()
        {
            return 1;
        }
    }
}