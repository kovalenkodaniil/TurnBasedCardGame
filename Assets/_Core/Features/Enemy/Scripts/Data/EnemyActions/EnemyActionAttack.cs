using _Core.Features.Combat;
using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Enemy/Action/Create new attack")]
    public class EnemyActionAttack : EnemyActionPattern
    {
        public override void Apply(CombatCharacterManager characterManager, CombatEnemyCharacter enemy, int value)
        {
            characterManager.PlayerModel.TakeDamage(value);
        }
    }
}