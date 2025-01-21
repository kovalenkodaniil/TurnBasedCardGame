using Core.Data;

namespace _Core.Features.PlayerLogic
{
    public class Player
    {
        public static Player Instance {
            get
            {
                if (_instance == null)
                    _instance = new Player();

                return _instance;
            }
        }
        private static Player _instance;

        public readonly PlayerStartData startData;
        public readonly GameProgress gameProgress;
        
        public int CurrentHealth { get; private set; }
        

        public Player()
        {
            gameProgress = new GameProgress();
            startData = StaticDataProvider.Get<PlayerDataProvider>().startData;
            CurrentHealth = startData.startHealth;
        }

        public void SaveHealthAfterBattle(int value)
        {
            if (value > 0)
                CurrentHealth = value;
        }

        public void Reset()
        {
            CurrentHealth = startData.startHealth;
            gameProgress.Reset();
        }
    }
}