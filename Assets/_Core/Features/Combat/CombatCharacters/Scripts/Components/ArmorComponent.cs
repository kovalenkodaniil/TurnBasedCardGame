using System;
using R3;

namespace _Core.Features.Combat.CombatCharacters.Components
{
    public class ArmorComponent
    {
        private ReactiveProperty<int> _armor;
        public ReadOnlyReactiveProperty<int> Armor => _armor;

        public ArmorComponent()
        {
            _armor = new ReactiveProperty<int>(0);
        }

        public int DecreaseDamage(int damage)
        {
            if (damage <= 0)
                return 0;
            
            int modifiedDamage = Math.Max(0, damage - Armor.CurrentValue);
            _armor.Value = Math.Max(0, _armor.Value - damage);

            return modifiedDamage;
        }

        public void AddArmor(int value)
        {
            if (value <= 0)
                return;

            _armor.Value += value;
        }

        public void Reset() => _armor.Value = 0;
    }
}