using System.Collections.Generic;
using _Core.Features.Enemy.Scripts;
using _Core.Features.Enemy.Scripts.View;
using DG.Tweening;
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
                _actionViews[i].Description =
                    enemyAction.action.description.Replace("{value}", enemyAction.value.ToString());
                _actionViews[i].PlayAppearanceAnimation();
            }
        }

        public void PlayActionAnimation()
        {
            _actionViews.Find(view => view.isActiveAndEnabled).PlayExecutionAnimation();
        }

        public void PlayAttackAnimation()
        {
            _animation = _icon.transform.DOLocalMoveX(-30, 0.3f).SetRelative().SetLoops(2, LoopType.Yoyo);
        }
    }
}