using System.Collections.Generic;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class CardPool
    {
        private List<Card> _cards;
        private Card _prefab;
        private Transform _baseParent;

        public void Init(Card cardPrefab, Transform parent)
        {
            _prefab = cardPrefab;
            _baseParent = parent;
            _cards = new List<Card>();

            CreateCard();
        }

        public Card GetCard()
        {
            Card newCard = _cards.Find(card => !card.gameObject.activeSelf);

            if (newCard == null)
                newCard = CreateCard();

            newCard.gameObject.SetActive(true);
            _cards.Remove(newCard);
            
            return newCard;
        }

        public void ReturnCard(Card card)
        {
            card.gameObject.SetActive(false);
            _cards.Add(card);
        }

        private Card CreateCard()
        {
            Card newCard = Object.Instantiate(_prefab, _baseParent);
            newCard.gameObject.SetActive(false);
            _cards.Add(newCard);

            return newCard;
        }
    }
}