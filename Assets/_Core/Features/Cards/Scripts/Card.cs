using System;
using _Core.Features.Combat;
using _Core.Features.Combat.CombatCharacters;
using _Core.Features.Combat.UI;
using Core.Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Features.Cards.Scripts
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public event Action<Card> OnUsed;
        public event Action<Card> OnDiscarded;

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _manaCost;
        [SerializeField] private Image _art;

        public CardConfig Config { get; private set; }
        public bool IsDiscarded { get; private set; }

        #region Dependencies

        private CombatCharacterManager _characterManager;
        private ManaCounter _manaCounter;
        private ArcRender _arcRender;
        private CardAssets _cardAssets;

        #endregion

        #region Cashe

        private Tween _tween;
        private Sequence _tweenSequence;
        private bool _isInteractable;

        #endregion


        public void Init(CardConfig cardConfig, CombatCharacterManager characterManager, ManaCounter manaCounter)
        {
            gameObject.SetActive(true);
            
            Config = cardConfig;
            _name.text = cardConfig.name;
            _manaCost.text = cardConfig.manaCost.ToString();
            _art.sprite = cardConfig.icon;
            SetDescription();
            
            _characterManager = characterManager;
            _manaCounter = manaCounter;
            _cardAssets = StaticDataProvider.Get<CardDataProvider>().cardAssets;
        }

        public void OnDestroy()
        {
            _tween?.Kill();
            _tweenSequence?.Kill();
        }

        public void SetAcrRender(ArcRender arcRender)
        {
            _arcRender = arcRender;
        }

        public void PlayDiscardAnimation(Vector3 discardPosition)
        {
            _isInteractable = false;
            IsDiscarded = true;
            _tweenSequence = DOTween.Sequence();

            Vector3 midPoint = (transform.position + discardPosition) / 2;
            midPoint.y += _cardAssets.midPointHeight;
            
            Vector3[] path = new Vector3[]
            {
                midPoint,
                discardPosition
            };
            
            _tweenSequence.Append(transform.DOPath(path, _cardAssets.discardDuration, PathType.CatmullRom)).SetEase(Ease.Linear);
            _tweenSequence.Join(transform.DOScale(_cardAssets.discardScale, _cardAssets.discardDuration));
            _tweenSequence.Join(transform.DORotate(_cardAssets.discardRotation, _cardAssets.discardDuration));

            _tweenSequence.OnComplete(() => OnDiscarded?.Invoke(this));
        }

        public void PlayDrawAnimation(Transform startPosition, Vector3 handPosition, Transform newParent, float drawDelay)
        {
            IsDiscarded = false;
            _tweenSequence = DOTween.Sequence();

            transform.SetParent(startPosition);
            transform.position = startPosition.position;
            transform.localScale = new Vector3(_cardAssets.discardScale,_cardAssets.discardScale,_cardAssets.discardScale);
            transform.rotation = Quaternion.Euler(_cardAssets.drawRotation);

            _tweenSequence.Append(transform.DOMove(handPosition, _cardAssets.drawDuration)).SetDelay(drawDelay);
            _tweenSequence.Join(transform.DOScale(_cardAssets.defaultScale, _cardAssets.drawDuration));
            _tweenSequence.Join(transform.DORotate(Vector3.zero, _cardAssets.drawDuration));

            _tweenSequence.OnComplete(() =>
            {
                transform.SetParent(newParent);
                _isInteractable = true;
            });
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            transform.DOScale(_cardAssets.highlightScale, _cardAssets.highlightDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            transform.DOScale(_cardAssets.defaultScale, _cardAssets.highlightDuration);
        }

        public void OnBeginDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData)
        {
            _arcRender.DisableArc();
            
            CombatBaseCharacter target = null;
            
            switch (Config.targetType)
            {
                case EnumTargetType.Player:
                    if (_characterManager.IsMouseOnPlayer(out CombatBaseCharacter player))
                        target = player;
                    break;
                
                case EnumTargetType.Enemy:
                    if (_characterManager.IsMouseOnEnemy(out CombatBaseCharacter enemy))
                        target = enemy;
                    break;
            }

            if (target != null)
            {
                TryToUse(target);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            _arcRender.DrawArc(transform.position);
        }

        private void SetDescription()
        {
            string description;
            _description.text = "";
            
            Config.effects.ForEach(effect =>
            {
                description = effect.effect.Description.Replace("{value}", effect.value.ToString());
                _description.text += description;
            });
        }

        private void TryToUse(CombatBaseCharacter character)
        {
            if (_manaCounter.TrySpend(Config.manaCost))
            {
                Config.effects.ForEach(effect => { effect.Apply(character); });
            
                OnUsed?.Invoke(this);
            }
        }
    }
}