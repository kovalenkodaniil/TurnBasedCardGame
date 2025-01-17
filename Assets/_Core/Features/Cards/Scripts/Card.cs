using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Features.Cards.Scripts
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Subject<Card> OnUsed = new Subject<Card>();
        public Subject<Card> OnDiscarded = new Subject<Card>();

        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _manaCost;
        [SerializeField] private Image _art;

        /*public string Name { set => _name.text = value; }
        public string Description { set => _description.text = value; }
        public string Cost { set => _manaCost.text = value; }
        public Sprite Art { set => _art.sprite = value; }*/

        public CardConfig Config { get; private set; }

        public void Init(CardConfig cardConfig)
        {
            gameObject.SetActive(true);
            
            _name.text = cardConfig.name;
            _description.text = cardConfig.effects[0].effect.Description;
            _manaCost.text = cardConfig.manaCost.ToString();
            _art.sprite = cardConfig.icon;

            Config = cardConfig;
        }

        public void PlayDiscardAnimation(Vector3 discardPosition)
        {
            transform.DOMove(discardPosition, 0.4f).OnComplete(() =>
            {
                OnDiscarded.OnNext(this);
                OnDiscarded.OnCompleted();
            });
        }

        public void ClearSubs()
        {
            OnUsed.OnCompleted();
            OnDiscarded.OnCompleted();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Фокус ин");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Фокус out");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            
            OnUsed.OnNext(this);
            OnUsed.OnCompleted();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
        }
    }
}