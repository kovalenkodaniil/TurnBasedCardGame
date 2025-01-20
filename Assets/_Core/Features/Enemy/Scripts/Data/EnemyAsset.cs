using System.Collections.Generic;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "Enemy Asset", menuName = "Enemy/Create enemy asset")]
    public class EnemyAsset : ScriptableObject
    {
        [Header("Enemy List")] 
        public List<EnemyConfig> enemyConfigs;
    }
}