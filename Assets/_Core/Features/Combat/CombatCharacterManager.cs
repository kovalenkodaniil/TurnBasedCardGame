using System.Collections.Generic;
using _Core.Features.Combat.CombatCharacters;
using _Core.Features.Encounters;
using _Core.Features.Enemy.Scripts;
using R3;
using R3.Triggers;
using UnityEngine;
using Player = _Core.Features.PlayerLogic.Player;

namespace _Core.Features.Combat
{
    public class CombatCharacterManager : MonoBehaviour
    {
        public Subject<Unit> OnEnemyTurnFinished = new();
        public Subject<Unit> OnPlayerDefeated = new();
        public Subject<Unit> OnAllEnemyDefeated = new();

        [SerializeField] private Transform _playerCharacterParent;
        [SerializeField] private Transform _enemyCharactersParent;
        [SerializeField] private CombatCharacterView _playerPrefab;
        [SerializeField] private CombatEnemyView _enemyPrefab;

        private CombatCharacterView _player;
        private List<EnemyCombatPresenter> _enemies;

        public CombatBaseCharacter PlayerModel => _player.Model;

        public void StartBattle(BattleConfig battleConfig)
        {
            CreatePlayerCharacter();
            CreateEnemiesCharacters(battleConfig);
        }

        public void OnDisable()
        {
            this.OnDisableAsObservable();
        }

        public void Reset()
        {
            _player.Model.Destroy();
            Destroy(_player.gameObject);
            _enemies.ForEach(enemy => enemy.Disable());
        }

        public bool IsMouseOnPlayer(out CombatBaseCharacter player)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_player.IsPositionOnCharacter(mousePosition))
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

            foreach (var enemyPresenter in _enemies)
            {
                if (enemyPresenter.IsMouseOnEnemy(mousePosition))
                {
                    enemy = enemyPresenter.Model;
                    return true;
                }
            }

            enemy = null;
            return false;
        }

        public void PrepareNewTurn()
        {
            _player.Model.StartNewTurn();
            _enemies.ForEach(enemy => enemy.UpdateIntentions());
        }

        public void StartEnemyTurn()
        {
            if (_enemies.Count > 0)
                _enemies[0].StartTurn();
            else
                EndEnemyTurn();
        }

        private void StartNextEnemyTurn(EnemyCombatPresenter lastEnemy)
        {
            int enemyIndex = _enemies.IndexOf(lastEnemy) + 1;

            if (enemyIndex >= _enemies.Count)
            {
                EndEnemyTurn();
                return;
            }
            
            _enemies[enemyIndex].StartTurn();
        }

        private void CreatePlayerCharacter()
        {
            _player = Instantiate(_playerPrefab, _playerCharacterParent);

            _player.Init(new CombatPlayerCharacter(Player.Instance.CurrentHealth, Player.Instance.startData.startHealth));
            _player.SetAvatar(Player.Instance.startData.art);
            
            _player.Model.OnDied
                .Subscribe(character =>
                {
                    character.Destroy(); 
                    OnPlayerDefeated.OnNext(Unit.Default);
                })
                .AddTo(this);
        }

        private void CreateEnemiesCharacters(BattleConfig battleConfig)
        {
            _enemies = new List<EnemyCombatPresenter>();
            
            battleConfig.enemies.ForEach(enemyConfig =>
            {
                CombatEnemyView view = Instantiate(_enemyPrefab, _enemyCharactersParent);
                EnemyCombatPresenter enemy = new EnemyCombatPresenter(enemyConfig, view, this);
                
                enemy.OnTurnEnded
                    .Subscribe(StartNextEnemyTurn)
                    .AddTo(this);

                enemy.OnDefeated
                    .Subscribe(CheckEnemiesDefeatCondition)
                    .AddTo(this);
            
                _enemies.Add(enemy);
            });
        }

        private void EndEnemyTurn()
        {
            OnEnemyTurnFinished.OnNext(Unit.Default);
        }

        private void CheckEnemiesDefeatCondition(EnemyCombatPresenter enemy)
        {
            _enemies.Remove(enemy);
            
            if (_enemies.Count <= 0)
                OnAllEnemyDefeated.OnNext(Unit.Default);
        }
    }
}