using System.Collections;
using System.Collections.Generic;
using _Core.Features.Combat;
using _Core.Features.Combat.UI;
using Core.Data;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class PlayerHand: MonoBehaviour
    {
        [SerializeField] private Transform _cardParent;
        [SerializeField] private Transform _discardPile;
        [SerializeField] private Transform _pilePosition;
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private ArcRender _arcRender;
        
        private List<Card> _cards;
        private CardPool _cardPool;
        private Pile _pile;
        private CardAssets _cardAssets;
        private CombatCharacterManager _characterManager;
        private ManaCounter _manaCounter;

        public int CardInHand { get; private set; }

        public void Init(Pile pile, CombatCharacterManager characterManager, ManaCounter manaCounter)
        {
            _arcRender.Init();
            
            _cards = new List<Card>();
            CardInHand = 0;
            _cardPool = new CardPool();
            _cardPool.Init(_cardPrefab, _cardParent, _arcRender);
            
            _pile = pile;
            _characterManager = characterManager;
            _manaCounter = manaCounter;
            _cardAssets = StaticDataProvider.Get<CardDataProvider>().cardAssets;
        }

        public void Reset()
        {
            _cards.ForEach(card =>
            {
                Destroy(card.gameObject);
            });
        }

        public void DrawHand()
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard(i*_cardAssets.drawDelay);
            }
        }
        
        public void ClearHand()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                if (!_cards[i].IsDiscarded)
                {
                    DiscardCard(_cards[i]);
                }
            }
        }

        public void DrawCard(float drawDelay = 0f)
        {
            Card newCard = _cardPool.GetCard();
            
            newCard.Init(_pile.DrawCard(), _characterManager, _manaCounter);
            newCard.PlayDrawAnimation(_pilePosition, _cardParent.position, _cardParent, drawDelay);

            CardInHand++;
            newCard.OnUsed += DiscardCard;
            
            _cards.Add(newCard);
        }

        private void DiscardCard(Card discardedCard)
        {
            discardedCard.transform.SetParent(_discardPile);
            discardedCard.PlayDiscardAnimation(_discardPile.position);

            discardedCard.OnDiscarded += ReturnCardToPool;
        }

        private void ReturnCardToPool(Card card)
        {
            card.OnDiscarded -= ReturnCardToPool;
            card.OnUsed -= DiscardCard;
            
            _pile.DiscardCard(card.Config);
            _cardPool.ReturnCard(card);
            _cards.Remove(card);

            CardInHand--;
        }
    }
}