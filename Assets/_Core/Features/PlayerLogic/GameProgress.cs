using System.Collections.Generic;
using _Core.Features.Encounters;
using Core.Data;

namespace _Core.Features.PlayerLogic
{
    public class GameProgress
    {
        public BattleConfig CurrentBattle { get; private set; }

        private List<BattleConfig> _battles;

        public GameProgress()
        {
            _battles = new List<BattleConfig>();
            _battles.AddRange(StaticDataProvider.Get<BattleDataProvider>().battleAsset.battles);

            CurrentBattle = _battles[0];
        }

        public void Reset()
        {
            CurrentBattle = _battles[0];
        }

        public void CompleteBattle()
        {
            int battleIndex = _battles.IndexOf(CurrentBattle);
            battleIndex++;

            if (battleIndex >= _battles.Count)
                CurrentBattle = _battles[^1];
            else
                CurrentBattle = _battles[battleIndex];
        }
    }
}