using _Core.Features.Cards.Scripts;
using _Core.Features.Encounters;
using _Core.Features.Enemy.Data;
using _Core.Features.PlayerLogic;
using UnityEngine;

namespace Core.Data
{
    [DefaultExecutionOrder(-1)]
    public class StaticDataProviderInstaller : MonoBehaviour
    {
        [SerializeField] private CardAssets _cardAssets;
        [SerializeField] private EnemyAsset _enemyAsset;
        [SerializeField] private PlayerStartData _playerStartData;
        [SerializeField] private BattleAsset _battleAsset;
        
        private void Awake()
        {
            StaticDataProvider.Add(new CardDataProvider(_cardAssets));
            StaticDataProvider.Add(new EnemyDataProvider(_enemyAsset));
            StaticDataProvider.Add(new PlayerDataProvider(_playerStartData));
            StaticDataProvider.Add(new BattleDataProvider(_battleAsset));
        }
    }
}