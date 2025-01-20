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
        private CombatCharacterView _view;
        private EnemyConfig _config;
        private CombatCharacterManager _characterManager;

        public CombatEnemyCharacter Model => _model;

        public EnemyCombatPresenter(EnemyConfig config, CombatCharacterView view, CombatCharacterManager characterManager)
        {
            _config = config;
            _view = view;
            _model = new CombatEnemyCharacter(config);
            _characterManager = characterManager;

            OnTurnEnded = new Subject<EnemyCombatPresenter>();
            
            _view.Init(_model);
        }

        public bool IsMouseOnEnemy(Vector3 mousePosition) => _view.IsPositionOnCharacter(mousePosition);

        public void StartTurn()
        {
            _model.StartTurn();

            StartCoroutine(TurnRoutine());
        }
        
        private IEnumerator TurnRoutine()
        {
            while (_model.IsHaveIntentions)
            {
                bool isExecuted = default;
                _model.ExecuteAction(_characterManager, () => isExecuted = true);
                yield return new WaitUntil(() => isExecuted);
                //view.HideFirstIntention();
                yield return new WaitForSeconds(0.5f);   
            }

            OnTurnEnded.OnNext(this);
            _model.EndTurn();
        }
    }
}