using System;
using _Core.Features.Cards.Scripts;
using UnityEngine;

namespace _Core.Features.Combat
{
    public class CombatEntryPoint : MonoBehaviour
    {
        [SerializeField] private PileManager _pileManager;
        [SerializeField] private CombatCharacterManager _combatCharacterManager;

        public void Awake()
        {
            _combatCharacterManager.Init();
            _pileManager.Init(_combatCharacterManager);
        }
        
    }
}