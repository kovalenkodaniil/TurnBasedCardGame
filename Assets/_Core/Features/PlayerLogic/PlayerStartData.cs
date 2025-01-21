using System.Collections.Generic;
using _Core.Features.Cards.Scripts;
using UnityEngine;

namespace _Core.Features.PlayerLogic
{
    [CreateAssetMenu(fileName = "Player start data", menuName = "Player/Create player start data")]
    public class PlayerStartData : ScriptableObject
    {
        public Sprite art;
        public int startHealth;
        public int manaPerRound;
        public List<CardConfig> startCards;
    }
}