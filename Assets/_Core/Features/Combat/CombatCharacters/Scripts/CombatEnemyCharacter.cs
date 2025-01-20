using System;
using System.Collections.Generic;
using _Core.Features.Combat.CombatCharacters.Components;
using _Core.Features.Enemy.Data;
using _Core.Features.Enemy.Scripts;
using R3;

namespace _Core.Features.Combat.CombatCharacters
{
    public class CombatEnemyCharacter : CombatBaseCharacter
    {
        private EnemyConfig data;
        private int _currentTurnIndex;
        private Queue<EnemyAction> _enemyIntentions;

        public bool IsHaveIntentions => _enemyIntentions.Count > 0;

        public CombatEnemyCharacter(EnemyConfig config)
        {
            OnDied = new Subject<CombatBaseCharacter>();
            disposable = new CompositeDisposable();

            data = config;
            HealthComponent = new HealthComponent(config.health, config.health);
            ArmorComponent = new ArmorComponent();
            _currentTurnIndex = 0;
            _enemyIntentions = new Queue<EnemyAction>();

            HealthComponent.OnDied
                .Subscribe(_ => Die())
                .AddTo(disposable);
        }

        public void StartTurn()
        {
            if (_currentTurnIndex > data.turns.Count)
                _currentTurnIndex = 0;
            
            data.turns[_currentTurnIndex].actions.ForEach(action =>
            {
                _enemyIntentions.Enqueue(action);
            });
        }

        public void EndTurn()
        {
            _enemyIntentions.Clear();
        }

        public void ExecuteAction(CombatCharacterManager characterManager, Action callback = null)
        {
            _enemyIntentions.Dequeue().Apply(characterManager, this);
            
            callback?.Invoke();
        }

        private void Die()
        {
            disposable.Dispose();
            
            OnDied.OnNext(this);
            OnDied.OnCompleted();
        }
    }
}