using _Core.Features.Combat;
using _Core.Features.Enemy.Scripts;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Enemy/Action/Create new heal")]
    public class EnemyActionHeal : EnemyActionPattern
    {
        public override void Apply(CombatCharacterManager characterManager, EnemyCombatPresenter enemy, int value)
        {
            enemy.Model.Heal(value);
        }
    }
}