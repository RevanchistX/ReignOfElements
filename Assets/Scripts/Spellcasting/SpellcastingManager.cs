using UnityEngine;


namespace Spellcasting
{
    public class SpellcastingManager : MonoBehaviour
    {
        private static SpellcastingManager instance;
        public static SpellcastingManager Instance => instance;
        private SpellcastingState spellcastingState = new();
        public SpellcastingState SpellcastingState => spellcastingState;

        private void Awake()
        {
            instance ??= this;
        }

        private void Update()
        {
            if (Input.GetKey("c"))
            {
                ChangeState(new CastingState());
            }
            else if (Input.GetKey("h"))
            {
                ChangeState(new HarvestingState());
            }
            else if (Input.GetKey("n"))
            {
                ChangeState(new SpellcastingState());
            }
        }

        public void ChangeState(SpellcastingState newSpellcastingState)
        {
            Debug.Log($"Changing state to: {newSpellcastingState}");
            spellcastingState = newSpellcastingState;
        }
    }
}