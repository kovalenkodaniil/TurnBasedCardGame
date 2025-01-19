using _Core.Features.Combat.CombatCharacters.Components;
using R3;

namespace _Core.Features.Combat.CombatCharacters
{
    public abstract class CombatBaseCharacter
    {
        public Subject<CombatBaseCharacter> OnDied;

        public HealthComponent HealthComponent { get; protected set; }
        public ArmorComponent ArmorComponent { get; protected set; }

        protected CompositeDisposable disposable;

        #region BasicMethods

        public virtual void Init(){}

        public void TakeDamage(int damage) => HealthComponent.TakeDamage(ArmorComponent.DecreaseDamage(damage));

        public void Heal(int value) => HealthComponent.Heal(value);

        public void AddArmor(int value) => ArmorComponent.AddArmor(value);

        #endregion
    }
}