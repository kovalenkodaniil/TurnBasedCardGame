using System;
using _Core.Features.Combat;
using _Core.Features.Enemy.Scripts;

namespace _Core.Features.Enemy.Data
{
    [Serializable]
    public class EnemyAction
    {
        public EnemyActionPattern action;
        public int value;

        public void Apply(CombatCharacterManager characterManager, EnemyCombatPresenter enemy)
        {
            action.Apply(characterManager, enemy, value);
        }
    }
}