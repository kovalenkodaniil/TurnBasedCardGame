using System.Collections;
using _Core.Features.Combat;
using _Core.Features.Combat.CombatCharacters;
using _Core.Features.Enemy.Data;
using R3;
using UnityEngine;
using static _Core.Features.CoroutineManager.CoroutineManager;

namespace _Core.Features.Enemy.Scripts
{
    public class EnemyCombatPresenter
    {
        public Subject<EnemyCombatPresenter> OnTurnEnded;

        private CombatEnemyCharacter _model;
        private CombatEnemyView _view;
        private EnemyConfig _config;
        private CombatCharacterManager _characterManager;

        public CombatEnemyCharacter Model => _model;

        public EnemyCombatPresenter(EnemyConfig config, CombatEnemyView view, CombatCharacterManager characterManager)
        {
            _config = config;
            _view = view;
            _model = new CombatEnemyCharacter(config);
            _characterManager = characterManager;

            OnTurnEnded = new Subject<EnemyCombatPresenter>();
            
            _view.Init(_model);
        }

        public bool IsMouseOnEnemy(Vector3 mousePosition) => _view.IsPositionOnCharacter(mousePosition);

        public void UpdatePlayerAction()
        {
            _model.UpdateIntentions();
            _view.SetNewAction(_config.turns[_model.CurrentTurnIndex]);
        }

        public void StartTurn()
        {
            StartCoroutine(TurnRoutine());
        }
        
        private IEnumerator TurnRoutine()
        {
            Debug.Log($"_model.IsHaveIntentions ={_model.IsHaveIntentions}");
            while (_model.IsHaveIntentions)
            {
                bool isExecuted = default;
                _model.ExecuteAction(_characterManager, () => isExecuted = true);
                yield return new WaitUntil(() => isExecuted);
                _view.PlayActionAnimation();
                yield return new WaitForSeconds(0.5f);   
            }

            _model.EndTurn();
            OnTurnEnded.OnNext(this);
        }
    }
}