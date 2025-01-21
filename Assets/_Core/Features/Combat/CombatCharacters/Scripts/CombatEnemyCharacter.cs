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
        private Queue<EnemyAction> _enemyIntentions;

        public bool IsHaveIntentions => _enemyIntentions.Count > 0;
        public int CurrentTurnIndex { get; private set; }

        public CombatEnemyCharacter(EnemyConfig config) : base()
        {
            data = config;
            HealthComponent = new HealthComponent(config.health, config.health);
            ArmorComponent = new ArmorComponent();
            CurrentTurnIndex = 0;
            _enemyIntentions = new Queue<EnemyAction>();

            HealthComponent.OnDied
                .Subscribe(_ => Die())
                .AddTo(disposable);
        }

        public void UpdateIntentions()
        {
            if (CurrentTurnIndex >= data.turns.Count)
                CurrentTurnIndex = 0;
            
            data.turns[CurrentTurnIndex].actions.ForEach(action =>
            {
                _enemyIntentions.Enqueue(action);
            });
        }

        public void EndTurn()
        {
            _enemyIntentions.Clear();
            CurrentTurnIndex++;
        }

        public void ExecuteAction(CombatCharacterManager characterManager, EnemyCombatPresenter presenter, Action callback = null)
        {
            _enemyIntentions.Dequeue().Apply(characterManager, presenter);
            
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