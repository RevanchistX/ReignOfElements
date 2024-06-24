namespace Spellcasting
{
    public class CastingState : SpellcastingState
    {
        public override bool IsCasting()
        {
            return true;
        }

        public override float GetMovementModifier()
        {
            return 0;
        }
    }
}