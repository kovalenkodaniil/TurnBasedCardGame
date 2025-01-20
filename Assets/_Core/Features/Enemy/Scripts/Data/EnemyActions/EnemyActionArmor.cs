using _Core.Features.Combat;
using _Core.Features.Combat.CombatCharacters;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Enemy/Action/Create new armor")]
    public class EnemyActionArmor : EnemyActionPattern
    {
        public override void Apply(CombatCharacterManager characterManager, CombatEnemyCharacter enemy, int value)
        {
            enemy.AddArmor(value);
        }
    }
}