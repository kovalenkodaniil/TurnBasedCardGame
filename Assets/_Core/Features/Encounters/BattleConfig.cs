using System.Collections.Generic;
using _Core.Features.Enemy.Data;
using UnityEngine;

namespace _Core.Features.Encounters
{
    [CreateAssetMenu(fileName = "New battle", menuName = "Battle/Create new battle")]
    public class BattleConfig : ScriptableObject
    {
        public List<EnemyConfig> enemies;
    }
}