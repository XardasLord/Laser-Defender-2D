using UnityEngine;

namespace Assets.Scripts
{
    public class GameSession : MonoBehaviour
    {
        private int _score;

        private void Awake()
        {
            SetUpSingleton();
            
            Enemy.OnDied += HandleEnemyDie;
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

        private void HandleEnemyDie(Enemy enemy)
        {
            AddScore(enemy.ScoreValue);
        }
    }
}
