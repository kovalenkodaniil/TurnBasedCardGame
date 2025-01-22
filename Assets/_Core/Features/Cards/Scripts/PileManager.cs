using System;
using System.Collections;
using _Core.Features.Combat;
using _Core.Features.PlayerLogic;
using Core.Data;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class PileManager : MonoBehaviour
    {
        [SerializeField] private PlayerHand _playerHand;
        [SerializeField] private PileUI _pileUI;

        private Pile _pile;
        private CardAssets _cardAssets;

        public void Init(CombatCharacterManager characterManager, ManaCounter manaCounter)
        {
            _pile = new Pile(Player.Instance.startData.startCards);

            _pileUI.Init(_pile);
            _playerHand.Init(_pile, characterManager, manaCounter);
            _cardAssets = StaticDataProvider.Get<CardDataProvider>().cardAssets;
        }

        public void Reset()
        {
            _pile.Destroy();
            _playerHand.Reset();
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
            
                yield return new WaitForSeconds(_cardAssets.discardDuration);
            }

            callback?.Invoke();
        }
    }
}