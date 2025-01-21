using _Core.Features.Cards.Scripts;
using _Core.Features.PlayerLogic;
using _Core.Features.Popups;
using UnityEngine;

namespace _Core.Features.Combat
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private PopupWin _popupWin;
        [SerializeField] private PopupLose _popupLose;

        private CombatCharacterManager _characterManager;
        private PileManager _pileManager;
        private TurnManager _turnManager;
        private CombatEventBus _combatEventBus;
        private ManaCounter _manaCounter;

        public void Init(CombatCharacterManager characterManager
            , PileManager pileManager
            , TurnManager turnManager
            , CombatEventBus combatEventBus
            , ManaCounter manaCounter)
        {
            _characterManager = characterManager;
            _pileManager = pileManager;
            _turnManager = turnManager;
            _combatEventBus = combatEventBus;
            _manaCounter = manaCounter;
        }

        public void StartBattle()
        {
            _characterManager.StartBattle(Player.Instance.gameProgress.CurrentBattle);
            _pileManager.Init(_characterManager, _manaCounter);
            _combatEventBus.StartBattle();
            
            _turnManager.StartBattle();
        }

        public void WinPLayer()
        {
            _popupWin.Open();
            
            Player.Instance.SaveHealthAfterBattle(_characterManager.PlayerModel.HealthComponent.Health.CurrentValue);
            Player.Instance.gameProgress.CompleteBattle();

            FinishBattle();
        }

        public void LosePlayer()
        {
            _popupLose.Open();
            Player.Instance.Reset();
            
            FinishBattle();
        }

        private void FinishBattle()
        {
            _characterManager.Reset();
            _pileManager.Reset();
            _combatEventBus.Dispose();
        }
    }
}