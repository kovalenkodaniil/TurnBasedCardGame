using System;
using Core.Data;
using UnityEngine;

namespace _Core.Features.Cards.Scripts
{
    public class PileManager : MonoBehaviour
    {
        [SerializeField] private PlayerHand _playerHand;
        [SerializeField] private PileUI _pileUI;

        private Pile _pile;

        public void Awake()
        {
            Init();
        }

        public void Init()
        {
            _pile = new Pile(StaticDataProvider.Get<CardDataProvider>().cardAssets.startingPile);

            _pileUI.Init(_pile);
            _playerHand.Init(_pile);
        }
    }
}