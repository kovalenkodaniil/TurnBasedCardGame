using _Core.Features.Cards.Scripts;
using _Core.Features.Enemy.Data;
using UnityEngine;

namespace Core.Data
{
    [DefaultExecutionOrder(-1)]
    public class StaticDataProviderInstaller : MonoBehaviour
    {
        [SerializeField] private CardAssets _cardAssets;
        [SerializeField] private EnemyAsset _enemyAsset;
        
        private void Awake()
        {
            StaticDataProvider.Add(new CardDataProvider(_cardAssets));
            StaticDataProvider.Add(new EnemyDataProvider(_enemyAsset));
        }
    }
}