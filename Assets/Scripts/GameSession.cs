using Assets.Scripts.Enemy;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        private int _score;

        private void Awake()
        {
            SetUpSingleton();

            FindObjectOfType<EnemySpawner>().OnSpawned += HandleEnemySpawn;
        }

        private void SetUpSingleton()
        {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public int GetScore()
        {
            return _score;
        }

        public void AddScore(int scoreValue)
        {
            _score += scoreValue;
        }

        public void ResetGame()
        {
            Destroy(gameObject);
        }

        private void HandleEnemySpawn(Enemy.Enemy obj)
        {
            obj.OnDied += HandleEnemyDie;
        }

        private void HandleEnemyDie(Enemy.Enemy enemy)
        {
            AddScore(enemy.ScoreValue);
        }
    }
}
