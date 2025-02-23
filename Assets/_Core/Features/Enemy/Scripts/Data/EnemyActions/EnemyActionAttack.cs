﻿using _Core.Features.Combat;
using _Core.Features.Enemy.Scripts;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Enemy/Action/Create new attack")]
    public class EnemyActionAttack : EnemyActionPattern
    {
        public override void Apply(CombatCharacterManager characterManager, EnemyCombatPresenter enemy, int value)
        {
            enemy.Attack();
            characterManager.PlayerModel.TakeDamage(value);
        }
    }
}