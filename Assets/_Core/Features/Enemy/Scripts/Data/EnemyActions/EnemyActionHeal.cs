using _Core.Features.Combat;
using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Enemy/Action/Create new heal")]
    public class EnemyActionHeal : EnemyActionPattern
    {
        public override void Apply(CombatCharacterManager characterManager, CombatEnemyCharacter enemy, int value)
        {
            enemy.Heal(value);
        }
    }
}