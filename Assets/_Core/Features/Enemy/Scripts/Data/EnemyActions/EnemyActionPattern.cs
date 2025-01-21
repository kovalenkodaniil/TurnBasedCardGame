using _Core.Features.Combat;
using _Core.Features.Combat.CombatCharacters;
using _Core.Features.Enemy.Scripts;
using UnityEngine;

namespace _Core.Features.Enemy.Data
{
    public abstract class EnemyActionPattern : ScriptableObject
    {
        public string description;
        public Sprite icon;

        public virtual void Apply(CombatCharacterManager characterManager, EnemyCombatPresenter enemy, int value)
        {
        }
    }
}