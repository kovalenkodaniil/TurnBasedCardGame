using _Core.Features.PlayerLogic;

namespace Core.Data
{
    public class PlayerDataProvider: IStaticDataProvider
    {
        public PlayerStartData startData;
        
        public PlayerDataProvider(PlayerStartData playerData)
        {
            startData = playerData;
        }
    }
}