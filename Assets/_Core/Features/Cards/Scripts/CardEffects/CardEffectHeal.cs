using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    [CreateAssetMenu(fileName = "Heal effect", menuName = "Card/Effects/Create heal effect")]
    public class CardEffectHeal : CardEffect
    {
        public override void Apply(CombatBaseCharacter character, int effectValue = 0)
        {
            character.Heal(effectValue);
        }
    }
}