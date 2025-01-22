using _Core.Features.Combat.CombatCharacters.Components;
using R3;

namespace _Core.Features.Combat.CombatCharacters
{
    public abstract class CombatBaseCharacter
    {
        public Subject<CombatBaseCharacter> OnDied;
        public Subject<int> OnAttacked;

        public HealthComponent HealthComponent { get; protected set; }
        public ArmorComponent ArmorComponent { get; protected set; }

        protected CompositeDisposable disposable;

        public CombatBaseCharacter()
        {
            OnDied = new Subject<CombatBaseCharacter>();
            OnAttacked = new Subject<int>();
            disposable = new CompositeDisposable();
        }

        #region BasicMethods

        public void Destroy()
        {
            OnDied.OnCompleted();
            OnAttacked.OnCompleted();
        }

        public void TakeDamage(int damage)
        {
            damage = ArmorComponent.DecreaseDamage(damage);
            OnAttacked.OnNext(damage);
            HealthComponent.TakeDamage(damage);
        }

        public void Heal(int value) => HealthComponent.Heal(value);

        public void AddArmor(int value) => ArmorComponent.AddArmor(value);

        public void StartNewTurn() => ArmorComponent.Reset();

        #endregion
    }
}