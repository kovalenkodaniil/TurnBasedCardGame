using System;
using System.Collections.Generic;
using _Core.Features.Combat.CombatCharacters;
using _Core.Features.Enemy.Data;
using _Core.Features.Enemy.Scripts;
using Core.Data;
using R3;
using R3.Triggers;
using UnityEngine;

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

        public CombatBaseCharacter Player => _player.Model;

        public void Init()
        {
            CreatePlayerCharacter();
            CreateEnemiesCharacters();
        }

        public void OnDisable()
        {
            this.OnDisableAsObservable();
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
            _enemies.ForEach(enemy => enemy.UpdatePlayerAction());
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
            
            _player.Init(new CombatPlayerCharacter(10,10));
            
            _player.Model.OnDied
                .Subscribe(character =>
                {
                    character.Destroy(); 
                    OnPlayerDefeated.OnNext(Unit.Default);
                })
                .AddTo(this);
        }

        private void CreateEnemiesCharacters()
        {
            _enemies = new List<EnemyCombatPresenter>();
            
            CombatEnemyView view = Instantiate(_enemyPrefab, _enemyCharactersParent);

            EnemyConfig config = StaticDataProvider.Get<EnemyDataProvider>().enemyAsset.enemyConfigs[0];
            EnemyCombatPresenter enemy = new EnemyCombatPresenter(config, view, this);

            enemy.OnTurnEnded
                .Subscribe(StartNextEnemyTurn)
                .AddTo(this);

            enemy.OnDefeated
                .Subscribe(CheckEnemiesDefeatCondition)
                .AddTo(this);
            
            _enemies.Add(enemy);
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