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
        public Subject<EnemyCombatPresenter> OnDefeated;

        #region Dependencies

        private CombatEnemyCharacter _model;
        private CombatEnemyView _view;
        private EnemyConfig _config;
        private CombatCharacterManager _characterManager;

        #endregion

        private CompositeDisposable _disposable;
        private Coroutine _turnCoroutine;
        
        public CombatEnemyCharacter Model => _model;

        public EnemyCombatPresenter(EnemyConfig config, CombatEnemyView view, CombatCharacterManager characterManager)
        {
            _config = config;
            _view = view;
            _model = new CombatEnemyCharacter(config);
            _characterManager = characterManager;

            _view.Init(_model);
            _view.SetAvatar(config.art);
            
            OnTurnEnded = new Subject<EnemyCombatPresenter>();
            OnDefeated = new Subject<EnemyCombatPresenter>();
            _disposable = new CompositeDisposable();
            _model.OnDied.Subscribe(Defeat).AddTo(_disposable);
        }

        public void Disable() => Destroy();

        public bool IsMouseOnEnemy(Vector3 mousePosition) => _view.IsPositionOnCharacter(mousePosition);

        public void UpdatePlayerAction()
        {
            _model.UpdateIntentions();
            _view.SetNewAction(_config.turns[_model.CurrentTurnIndex]);
        }

        public void StartTurn()
        {
            _turnCoroutine = StartCoroutine(TurnRoutine());
        }

        private void Defeat(CombatBaseCharacter character)
        {
            OnDefeated.OnNext(this);
            Destroy();
        }

        private void Destroy()
        {
            StopCoroutine(_turnCoroutine);
            Object.Destroy(_view.gameObject);
            _model.Destroy();
            
            _disposable.Dispose();
        }

        private IEnumerator TurnRoutine()
        {
            while (_model.IsHaveIntentions)
            {
                bool isExecuted = default;
                _model.ExecuteAction(_characterManager, () => isExecuted = true);
                yield return new WaitUntil(() => isExecuted);
                if (_view != null)
                    _view.PlayActionAnimation();
                yield return new WaitForSeconds(0.5f);   
            }

            _model.EndTurn();
            OnTurnEnded.OnNext(this);
        }
    }
}