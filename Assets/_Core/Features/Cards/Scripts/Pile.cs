using System.Collections.Generic;
using R3;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class Pile
    {
        public ReactiveProperty<int> PileCount = new ReactiveProperty<int>();
        public ReactiveProperty<int> DiscardCount = new ReactiveProperty<int>();
        
        private List<CardConfig> _cardsInPile;
        private List<CardConfig> _discardedCards;

        public Pile()
        {
            _cardsInPile = new List<CardConfig>();
            _discardedCards = new List<CardConfig>();
            
            PileCount.Value = _cardsInPile.Count;
            DiscardCount.Value = 0;
        }

        public Pile(List<CardConfig> cardsInPile)
        {
            _cardsInPile = new List<CardConfig>();
            _discardedCards = new List<CardConfig>();
            
            _cardsInPile.AddRange(cardsInPile);

            PileCount.Value = _cardsInPile.Count;
            DiscardCount.Value = 0;
        }

        public CardConfig DrawCard()
        {
            if (_cardsInPile.Count <= 0)
                Shuffle();
            
            CardConfig drawnCard = _cardsInPile[Random.Range(0, _cardsInPile.Count)];
            _cardsInPile.Remove(drawnCard);

            PileCount.Value = _cardsInPile.Count;
            
            return drawnCard;
        }

        public void DiscardCard(CardConfig discardedCard)
        {
            _discardedCards.Add(discardedCard);

            DiscardCount.Value = _discardedCards.Count;
        }

        public void Shuffle()
        {
            _cardsInPile.AddRange(_discardedCards);
            _discardedCards.Clear();
            
            DiscardCount.Value = _discardedCards.Count;
            PileCount.Value = _cardsInPile.Count;
        }
    }
}