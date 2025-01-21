using _Core.Features.PlayerLogic;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine;

namespace _Core.Features.Combat
{
    public class ManaCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counter;

        private ReactiveProperty<int> _mana;

        public void Init()
        {
            _mana = new ReactiveProperty<int>(Player.Instance.startData.manaPerRound);

            _mana.Subscribe(UpdateCounter).AddTo(this);
        }

        public void OnDisable()
        {
            this.OnDisableAsObservable();
        }

        public void ResetRoundValue() => _mana.Value = Player.Instance.startData.manaPerRound;

        public bool TrySpend(int value)
        {
            if (value < 0)
                return false;
            
            if (value > _mana.CurrentValue)
                return false;

            _mana.Value -= value;
            return true;
        }

        private void UpdateCounter(int value) => _counter.text = value.ToString();
    }
}