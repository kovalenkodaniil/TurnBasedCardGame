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

        [Header("Animation Setting")] 
        public float defaultScale;
        public float highlightScale;
        public float highlightDuration;
        public float discardDuration;
        public float midPointHeight;
        public float discardScale;
        public Vector3 discardRotation;
        public float drawDuration;
        public Vector3 drawRotation;
        public float drawDelay;
    }
}