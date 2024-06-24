namespace Spellcasting
{
    public class HarvestingState : SpellcastingState
    {
        public override bool IsHarvesting()
        {
            return true;
        }

        public override float GetMovementModifier()
        {
            return 0.5f;
        }
    }
}