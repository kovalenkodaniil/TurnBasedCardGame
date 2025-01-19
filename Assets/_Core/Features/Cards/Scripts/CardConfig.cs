using System.Collections.Generic;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    [CreateAssetMenu(fileName = "New card", menuName = "Card/Create new card")]
    public class CardConfig : ScriptableObject
    {
        public string name;
        public Sprite icon;
        public int manaCost;
        public EnumTargetType targetType;
        public List<CardEffectData> effects;
    }
    
    public enum EnumTargetType
    {
        Player = 0,
        Enemy = 1
    }
}