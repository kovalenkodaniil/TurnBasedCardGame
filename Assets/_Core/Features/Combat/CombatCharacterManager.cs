using System.Collections;
using System.Collections.Generic;
using _Core.Features.Combat.CombatCharacters;
using R3;
using UnityEngine;

namespace _Core.Features.Combat
{
    public class CombatCharacterManager : MonoBehaviour
    {
        public Subject<Unit> OnEnemyTurnFinished = new();

        [SerializeField] private Transform _playerCharacterParent;
        [SerializeField] private Transform _enemyCharactersParent;
        [SerializeField] private CombatCharacterView _playerPrefab;
        [SerializeField] private CombatCharacterView _enemyPrefab;

        private CombatCharacterView _player;
        private List<CombatCharacterView> _enemies;
        
        public void Init()
        {
            CreatePlayerCharacter();
            CreateEnemiesCharacters();
        }

        public bool IsMouseOnPlayer(out CombatBaseCharacter player)
        {
            Vector3 mpusePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_player.IsPositionOnCharacter(mpusePosition))
            {
                player = _player.Model;
                return true;
            }

            player = null;
            return false;
        }
        
        public bool IsMouseOnEnemy(out CombatBaseCharacter enemy)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            foreach (var enemyView in _enemies)
            {
                if (enemyView.IsPositionOnCharacter(mousePosition))
                {
                    enemy = enemyView.Model;
                    return true;
                }
            }

            enemy = null;
            return false;
        }

        public void StartEnemyTurn()
        {
            _enemies.ForEach(enemy =>
            {
                //
            });

            StartCoroutine(WaitDelay());
        }

        private void CreatePlayerCharacter()
        {
            _player = Instantiate(_playerPrefab, _playerCharacterParent);
            
            _player.Init(new CombatPlayerCharacter(10,10));
        }

        private void CreateEnemiesCharacters()
        {
            _enemies = new List<CombatCharacterView>();
            
            CombatCharacterView enemy = Instantiate(_enemyPrefab, _enemyCharactersParent);
            enemy.Init(new CombatEnemyCharacter(10));
            
            _enemies.Add(enemy);
        }

        private IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(0.8f);
            
            OnEnemyTurnFinished.OnNext(Unit.Default);
        }
    }
}