using System;
using System.Collections;
using _Core.Features.Combat;
using Core.Data;
using R3;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class PileManager : MonoBehaviour
    {
        [SerializeField] private PlayerHand _playerHand;
        [SerializeField] private PileUI _pileUI;

        private Pile _pile;

        public void Init(CombatCharacterManager characterManager)
        {
            _pile = new Pile(StaticDataProvider.Get<CardDataProvider>().cardAssets.startingPile);

            _pileUI.Init(_pile);
            _playerHand.Init(_pile, characterManager);
        }

        public void DrawNewHand()
        {
            _playerHand.DrawHand();
        }

        public void DiscardHand(Action callback)
        {
            StartCoroutine(PlayerTurnEndRoutine(callback));
        }

        private IEnumerator PlayerTurnEndRoutine(Action callback)
        {
            if (_playerHand.CardInHand > 0)
            {
                _playerHand.ClearHand();
            
                yield return new WaitForSeconds(0.4f);
            }

            callback?.Invoke();
        }
    }
}