using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public abstract class CardEffect : ScriptableObject
    {
        [field:SerializeField] public string Description { get; protected set; }

        public virtual void Apply(CombatBaseCharacter character, int effectValue = 0)
        {
        }
    }
}