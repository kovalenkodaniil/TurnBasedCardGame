using _Core.Features.Cards.Scripts;
using _Core.Features.Combat.UI;
using R3;

namespace _Core.Features.Combat
{
    public class CombatEventBus
    {
        #region Dependencies

        private TurnManager _turnManager;
        private CombatUI _combatUI;
        private PileManager _pileManager;
        private CombatCharacterManager _characterManager;

        #endregion

        private CompositeDisposable _disposable;

        public CombatEventBus()
        {
            _disposable = new CompositeDisposable();
        }

        public void Init(TurnManager turnManager, CombatUI combatUI, PileManager pileManager, CombatCharacterManager characterManager)
        {
            _turnManager = turnManager;
            _combatUI = combatUI;
            _pileManager = pileManager;
            _characterManager = characterManager;

            SetupTurnManager();
            SetupUI();
            SetupCharacterManager();
        }

        public void Dispose() => _disposable.Dispose();

        private void SetupTurnManager()
        {
            _turnManager.OnTurnStarted
                .Subscribe(_ =>
                {
                    _pileManager.DrawNewHand();
                    _turnManager.NextStep();
                })
                .AddTo(_disposable);
            
            _turnManager.OnPlayerActionStarted
                .Subscribe(_ =>
                {
                    _combatUI.EnableEndTurnButton();
                })
                .AddTo(_disposable);
            
            _turnManager.OnEnemyActionStarted
                .Subscribe(_ =>
                {
                    _characterManager.StartEnemyTurn();
                })
                .AddTo(_disposable);
            
            _turnManager.OnTurnEnded
                .Subscribe(_ =>
                {
                    _turnManager.NextStep();
                })
                .AddTo(_disposable);
        }
        
        private void SetupUI()
        {
            _combatUI.OnEndTurnPressed
                .Subscribe(_ =>
                {
                    _pileManager.DiscardHand();
                    _turnManager.NextStep();
                })
                .AddTo(_disposable);
        }

        private void SetupCharacterManager()
        {
            _characterManager.OnEnemyTurnFinished
                .Subscribe(_ =>
                {
                    _turnManager.NextStep();
                })
                .AddTo(_disposable);
        }
    }
}