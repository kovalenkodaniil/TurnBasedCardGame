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
        [SerializeField] private CombatEndHandler _combatEndHandler;

        private TurnManager _turnManager;
        private CombatEventBus _combatEventBus;

        public void Awake()
        {
            _turnManager = new TurnManager();
            _combatEventBus = new CombatEventBus();
            
            _combatUI.Init();
            _combatCharacterManager.Init();
            _pileManager.Init(_combatCharacterManager);
            _combatEventBus.Init(_turnManager, _combatUI, _pileManager, _combatCharacterManager, _combatEndHandler);

            StartBattle();
        }

        public void StartBattle()
        {
            _turnManager.StartBattle();
        }
    }
}