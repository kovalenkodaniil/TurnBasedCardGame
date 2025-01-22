using _Core.Features.Cards.Scripts;
using _Core.Features.Combat.UI;
using UnityEngine;

namespace _Core.Features.Combat
{
    public class CombatEntryPoint : MonoBehaviour
    {
        [SerializeField] private PileManager _pileManager;
        [SerializeField] private CombatCharacterManager _combatCharacterManager;
        [SerializeField] private CombatUI _combatUI;
        [SerializeField] private CombatManager _combatManager;
        [SerializeField] private ManaCounter _manaCounter;

        private TurnManager _turnManager;
        private CombatEventBus _combatEventBus;

        public void Start()
        {
            _turnManager = new TurnManager();
            _combatEventBus = new CombatEventBus();
            
            _combatUI.Init();
            _manaCounter.Init();
            _combatManager.Init(_combatCharacterManager, _pileManager, _turnManager, _combatEventBus, _manaCounter);
            _combatEventBus.Init(_turnManager, _combatUI, _pileManager, _combatCharacterManager, _combatManager, _manaCounter);

            StartBattle();
        }

        private void StartBattle()
        {
            _combatManager.StartBattle();
        }
    }
}