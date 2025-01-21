using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Features.Enemy.Scripts.View
{
    public class EnemyActionView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _powerCount;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Sprite Icon { set => _icon.sprite = value; }
        public string PowerCount { set => _powerCount.text = value; }

        private Sequence _animation;

        public void OnDestroy()
        {
            _animation.Pause();
            _animation.Kill();
        }

        public void PlayExecutionAnimation(Action callback = null)
        {
            _animation = DOTween.Sequence();

            _animation.Append(_rect.DOScale(0.2f, 0.3f).SetRelative());
            _animation.Join(_canvasGroup.DOFade(0, 0.3f));

            _animation.OnComplete(() =>
            {
                gameObject.SetActive(false); 
                callback?.Invoke();
            });
        }

        public void PlayAppearanceAnimation()
        {
            gameObject.SetActive(true);
            _rect.localScale = Vector3.zero;
            _canvasGroup.alpha = 0;

            _animation = DOTween.Sequence();

            _animation.Append(_rect.DOScale(1f, 0.2f));
            _animation.Join(_canvasGroup.DOFade(1, 0.2f));
        }
    }
}