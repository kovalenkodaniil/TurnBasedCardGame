using _Core.Features.Encounters;

namespace Core.Data
{
    public class BattleDataProvider : IStaticDataProvider
    {
        public BattleAsset battleAsset;

        public BattleDataProvider(BattleAsset battleAsset)
        {
            this.battleAsset = battleAsset;
        }
    }
}