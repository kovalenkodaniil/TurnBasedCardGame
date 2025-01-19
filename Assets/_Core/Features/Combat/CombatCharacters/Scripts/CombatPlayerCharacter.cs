using _Core.Features.Combat.CombatCharacters.Components;
using R3;

namespace _Core.Features.Combat.CombatCharacters
{
    public class CombatPlayerCharacter : CombatBaseCharacter
    {
        public CombatPlayerCharacter(int currentHealth, int maxHealth)
        {
            OnDied = new Subject<CombatBaseCharacter>();
            disposable = new CompositeDisposable();

            HealthComponent = new HealthComponent(currentHealth, maxHealth);
            ArmorComponent = new ArmorComponent();

            HealthComponent.OnDied
                .Subscribe(_ => Die())
                .AddTo(disposable);
        }

        private void Die()
        {
            disposable.Dispose();
            
            OnDied.OnNext(this);
            OnDied.OnCompleted();
        }
    }
}