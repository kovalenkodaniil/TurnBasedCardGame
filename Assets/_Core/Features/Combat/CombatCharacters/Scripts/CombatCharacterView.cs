using System;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Features.Combat.CombatCharacters
{
    public class CombatCharacterView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _armorCounter;
        [SerializeField] private TMP_Text _healthCounter;

        public CombatBaseCharacter Model { get; private set; }

        public void Init(CombatBaseCharacter character)
        {
            character.HealthComponent.Health
                .Subscribe(UpdateHealthCounter)
                .AddTo(this);
            
            character.ArmorComponent.Armor
                .Subscribe(UpdateArmorCounter)
                .AddTo(this);

            Model = character;
        }

        public void OnDisable()
        {
            this.OnDisableAsObservable();
        }

        public bool SetAvatar(Sprite avatar) => _icon.sprite = avatar;

        public bool IsPositionOnCharacter(Vector3 position)
        {
            Vector3 localPosition = _icon.rectTransform.InverseTransformPoint(position);

            return _icon.rectTransform.rect.Contains(localPosition);
        }

        private void UpdateHealthCounter(int currentHealth)
        {
            _healthCounter.text = currentHealth.ToString();
        }
        
        private void UpdateArmorCounter(int currentHealth)
        {
            _armorCounter.text = currentHealth.ToString();
        }
    }
}