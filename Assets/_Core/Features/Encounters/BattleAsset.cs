using System.Collections.Generic;
using UnityEngine;

namespace _Core.Features.Encounters
{
    [CreateAssetMenu(fileName = "Battle asset", menuName = "Battle/Create battle asset")]
    public class BattleAsset : ScriptableObject
    {
        [Header("Battle List")] 
        public List<BattleConfig> battles;
    }
}