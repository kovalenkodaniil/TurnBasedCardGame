using DG.Tweening;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Features.Combat.CombatCharacters
{
    public class CombatCharacterView : MonoBehaviour
    {
        [SerializeField] protected Image _icon;
        [SerializeField] private TMP_Text _armorCounter;
        [SerializeField] private TMP_Text _healthCounter;
        [SerializeField] private Slider _healthBar;

        public CombatBaseCharacter Model { get; private set; }
        protected Tween _animation;

        public void Init(CombatBaseCharacter character)
        {
            Model = character;
            SetupHealthBar(character);

            Model.OnAttacked
                .Subscribe(PlayDamageAnimation)
                .AddTo(this);
        }

        public void OnDisable()
        {
            _animation?.Kill();

            this.OnDisableAsObservable();
        }

        public bool SetAvatar(Sprite avatar) => _icon.sprite = avatar;

        public void PlayDamageAnimation(int damage) => _animation = _icon.transform.DOShakePosition(0.3f, 20f);

        public bool IsPositionOnCharacter(Vector3 position)
        {
            Vector3 localPosition = _icon.rectTransform.InverseTransformPoint(position);

            return _icon.rectTransform.rect.Contains(localPosition);
        }

        private void SetupHealthBar(CombatBaseCharacter character)
        {
            _healthBar.maxValue = character.HealthComponent.MaxHealth;
            
            character.HealthComponent.Health
                .Subscribe(UpdateHealthCounter)
                .AddTo(this);
            
            character.ArmorComponent.Armor
                .Subscribe(UpdateArmorCounter)
                .AddTo(this);
        }

        private void UpdateHealthCounter(int currentHealth)
        {
            _healthBar.value = currentHealth;
            _healthCounter.text = $"{currentHealth.ToString()}/{Model.HealthComponent.MaxHealth}";
        }
        
        private void UpdateArmorCounter(int currentArmor)
        {
            _armorCounter.text = $"{currentArmor}";
        }
    }
}