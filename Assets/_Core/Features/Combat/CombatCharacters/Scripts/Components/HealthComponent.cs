using System;
using R3;

namespace _Core.Features.Combat.CombatCharacters.Components
{
    public class HealthComponent
    {
        public Subject<Unit> OnDied;

        private ReactiveProperty<int> _health;
        public ReadOnlyReactiveProperty<int> Health => _health;

        public int MaxHealth { get; private set; }

        public HealthComponent(int currentHealth, int maxHealth)
        {
            _health = new ReactiveProperty<int>(currentHealth);
            MaxHealth = maxHealth;

            OnDied = new Subject<Unit>();
        }

        public void TakeDamage(int value)
        {
            if (value <= 0) return;
            
            _health.Value = Math.Max(0, _health.CurrentValue - value);
            
            if (_health.CurrentValue == 0)
                OnDied.OnNext(Unit.Default);
        }

        public void Heal(int value)
        {
            if (value <= 0) return;
            
            _health.Value = Math.Min(MaxHealth, _health.CurrentValue + value);
        }
    }
}