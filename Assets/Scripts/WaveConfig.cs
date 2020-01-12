using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Enemy Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject pathPrefab;
        [SerializeField] private float timeBetweenSpawns = 0.5f;
        [SerializeField] private float spawnRandomFactor = 0.3f;
        [SerializeField] private int numberOfEnemies = 5;
        [SerializeField] private float moveSpeed = 2f;

        public GameObject GetEnemyPrefab() => enemyPrefab;
        public GameObject GetPathPrefab() => pathPrefab;
        public float GetTimeBetweenSpawns() => timeBetweenSpawns;
        public float GetSpawnRandomFactor() => spawnRandomFactor;
        public int GetNumberOfEnemies() => numberOfEnemies;
        public float GetMoveSpeed() => moveSpeed;
    }
}
