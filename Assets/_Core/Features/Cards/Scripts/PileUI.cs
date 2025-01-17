using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Features.Cards.Scripts
{
    public class PileUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _pileCount;
        [SerializeField] private TMP_Text _discardPileCount;
        [SerializeField] private Image _pileShirts;
        [SerializeField] private Image _discardShirts;

        private Pile _pile;
        
        public void Init(Pile pile)
        {
            _pile = pile;
            
            _pile.PileCount
                .Subscribe(UpdatePileCount)
                .AddTo(this);
            
            _pile.DiscardCount
                .Subscribe(UpdateDiscardCount)
                .AddTo(this);
        }

        private void UpdatePileCount(int value)
        {
            _pileCount.text = value.ToString();
            
            _pileShirts.gameObject.SetActive(value > 0);
        }
        
        private void UpdateDiscardCount(int value)
        {
            _discardPileCount.text = value.ToString();
            
            _discardShirts.gameObject.SetActive(value > 0);
        }
    }
}