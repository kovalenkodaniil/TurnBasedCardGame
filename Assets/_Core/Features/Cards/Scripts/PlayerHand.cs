﻿using System.Collections.Generic;
using R3;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class PlayerHand: MonoBehaviour
    {
        [SerializeField] private Transform _cardParent;
        [SerializeField] private Transform _discardPile;
        [SerializeField] private Card _cardPrefab;
        
        private List<Card> _cards;
        private CardPool _cardPool;
        private Pile _pile;

        public void Init(Pile pile)
        {
            _cards = new List<Card>();
            _cardPool = new CardPool();
            _cardPool.Init(_cardPrefab, _cardParent);
            _pile = pile;

            for (int i = 0; i < 5; i++)
            {
                DrawCard();
            }
        }

        public void DrawCard()
        {
            Card newCard = _cardPool.GetCard();
            
            newCard.transform.SetParent(_cardParent);
            newCard.Init(_pile.DrawCard());

            newCard.OnUsed
                .Subscribe(DiscardCard)
                .AddTo(this);
            
            _cards.Add(newCard);
        }

        private void DiscardCard(Card discardedCard)
        {
            discardedCard.transform.SetParent(_discardPile);
            discardedCard.PlayDiscardAnimation(_discardPile.position);

            discardedCard.OnDiscarded
                .Subscribe(ReturnCardToPool)
                .AddTo(this);
        }

        private void ReturnCardToPool(Card card)
        {
            _pile.DiscardCard(card.Config);
            _cardPool.ReturnCard(card);
        }
    }
}