using System;
using _Core.Features.Combat.CombatCharacters;

namespace _Core.Features.Cards.Scripts
{
    [Serializable]
    public class CardEffectData
    {
        public CardEffect effect;
        public int value;

        public void Apply(CombatBaseCharacter character)
        {
            effect.Apply(character, value);
        }
    }
}