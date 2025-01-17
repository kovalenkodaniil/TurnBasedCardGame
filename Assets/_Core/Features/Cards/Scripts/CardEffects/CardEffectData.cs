using System;

namespace _Core.Features.Cards.Scripts
{
    [Serializable]
    public class CardEffectData
    {
        public CardEffect effect;
        public int value;

        public void Apply()
        {
            effect.Apply();
        }
    }
}