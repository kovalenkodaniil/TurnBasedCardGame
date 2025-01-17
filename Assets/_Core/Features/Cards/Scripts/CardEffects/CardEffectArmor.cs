using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    [CreateAssetMenu(fileName = "Armor effect", menuName = "Card/Effects/Create armor effect")]
    public class CardEffectArmor : CardEffect
    {
        public override void Apply(CombatBaseCharacter character,int effectValue = 0)
        {
            character.AddArmor(effectValue);
        }
    }
}