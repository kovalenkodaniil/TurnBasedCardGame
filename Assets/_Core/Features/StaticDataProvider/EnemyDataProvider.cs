using _Core.Features.Enemy.Data;

namespace Core.Data
{
    public class EnemyDataProvider : IStaticDataProvider
    {
        public EnemyAsset enemyAsset;

        public EnemyDataProvider(EnemyAsset enemyAsset)
        {
            this.enemyAsset = enemyAsset;
        }
    }
}