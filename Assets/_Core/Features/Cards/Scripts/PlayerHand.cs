using System.Collections.Generic;
using _Core.Features.Combat;
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
        private CombatCharacterManager _characterManager;

        public int CardInHand { get; private set; }

        public void Init(Pile pile, CombatCharacterManager characterManager)
        {
            _cards = new List<Card>();
            CardInHand = 0;
            _cardPool = new CardPool();
            _cardPool.Init(_cardPrefab, _cardParent);
            _pile = pile;
            _characterManager = characterManager;
        }

        public void Reset()
        {
            _cards.ForEach(card =>
            {
                card.ClearSubs();
                Destroy(card.gameObject);
            });
        }

        public void DrawHand()
        {
            for (int i = 0; i < 5; i++)
            {
                DrawCard();
            }
        }
        
        public void ClearHand()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].gameObject.activeSelf)
                {
                    DiscardCard(_cards[i]);
                }
            }
        }

        public void DrawCard()
        {
            Card newCard = _cardPool.GetCard();
            
            newCard.Init(_pile.DrawCard(), _characterManager);
            newCard.transform.SetParent(_cardParent);

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