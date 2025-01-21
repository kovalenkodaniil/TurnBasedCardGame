using System;
using System.Collections.Generic;
using _Core.Features.Combat;
using _Core.Features.Enemy.Data;

namespace _Core.Features.Enemy.Scripts
{
    [Serializable]
    public class EnemyTurn
    {
        public List<EnemyAction> actions;

        public void StartTurn(CombatCharacterManager characterManager, EnemyCombatPresenter enemy)
        {
            actions.ForEach(action => action.Apply(characterManager, enemy));
        }
    }
}