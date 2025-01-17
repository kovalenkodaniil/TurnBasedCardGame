using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    [CreateAssetMenu(fileName = "Attack effect", menuName = "Card/Effects/Create attack effect")]
    public class CardEffectAttack : CardEffect
    {
        public override void Apply(CombatBaseCharacter character, int effectValue = 0)
        {
            character.TakeDamage(effectValue);
        }
    }
}