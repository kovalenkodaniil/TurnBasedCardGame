using _Core.Features.Cards.Scripts;
using UnityEngine;

namespace Core.Data
{
    [DefaultExecutionOrder(-1)]
    public class StaticDataProviderInstaller : MonoBehaviour
    {
        [SerializeField] private CardAssets _cardAssets;
        
        private void Awake()
        {
            StaticDataProvider.Add(new CardDataProvider(_cardAssets));
        }
    }
}