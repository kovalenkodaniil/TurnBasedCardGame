using System.Collections.Generic;
using _Core.Features.Enemy.Scripts;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "New enemy", menuName = "Enemy/Create new enemy")]
    public class EnemyConfig : ScriptableObject
    {
        public Sprite art;
        public string name;
        public int health;
        public List<EnemyTurn> turns;
    }
}