using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Features.Combat.UI
{
    public class CombatUI : MonoBehaviour
    {
        public Subject<Unit> OnEndTurnPressed = new ();

        [SerializeField] private Button _endTurnButton;

        public void Init()
        {
            _endTurnButton.onClick.AddListener(EndTurn);
        }

        private void OnDisable()
        {
            _endTurnButton.onClick.RemoveListener(EndTurn);
            
            OnEndTurnPressed.OnCompleted();
        }

        private void EndTurn()
        {
            OnEndTurnPressed.OnNext(Unit.Default);
            
            EnableEndTurnButton(false);
        }

        public void EnableEndTurnButton(bool isEnabling = true)
        {
            _endTurnButton.interactable = isEnabling;
        }
    }
}