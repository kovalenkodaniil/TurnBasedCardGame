using System.Collections.Generic;
using _Core.Features.Enemy.Scripts;
using _Core.Features.Enemy.Scripts.View;
using UnityEngine;

namespace _Core.Features.Combat.CombatCharacters
{
    public class CombatEnemyView : CombatCharacterView
    {
        [SerializeField] private List<EnemyActionView> _actionViews;

        public void SetNewAction(EnemyTurn enemyTurn)
        {
            _actionViews.ForEach(view => view.gameObject.SetActive(false));
            
            for (var i = 0; i < enemyTurn.actions.Count; i++)
            {
                var enemyAction = enemyTurn.actions[i];
                
                _actionViews[i].Icon = enemyAction.action.icon;
                _actionViews[i].PowerCount = enemyAction.value.ToString();
                _actionViews[i].PlayAppearanceAnimation();
            }
        }

        public void PlayActionAnimation()
        {
            _actionViews.Find(view => view.isActiveAndEnabled).PlayExecutionAnimation();
        }
    }
}