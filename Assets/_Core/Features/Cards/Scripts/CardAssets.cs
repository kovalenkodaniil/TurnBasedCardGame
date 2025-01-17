using System.Collections.Generic;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    [CreateAssetMenu(fileName = "New card", menuName = "Card/Create card assets")]
    public class CardAssets : ScriptableObject
    {
        [Header("Sprites")]
        public Sprite cardFace;
        public Sprite cardShirts;

        [Header("Starting Pile")] 
        public List<CardConfig> startingPile;
    }
}