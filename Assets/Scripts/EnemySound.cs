using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySound : MonoBehaviour
    {
        [SerializeField] private AudioClip deathSound;
        [SerializeField] [Range(0, 1)] private float deathSoundVolume = 0.75f;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] [Range(0, 1)] private float shootSoundVolume = 0.75f;
        [SerializeField] private AudioClip spawnSound;
        [SerializeField] [Range(0, 1)] private float spawnSoundVolume = 0.75f;

        private void Awake()
        {
            var enemyComponent = GetComponent<Enemy>();

            enemyComponent.OnSpawned += HandleSpawn;
            enemyComponent.OnFired += HandleFire;
            Enemy.OnDied += HandleDie;
        }

        private void HandleSpawn(Enemy enemy)
        {
            if (enemy.IsBoss)
            {
                AudioSource.PlayClipAtPoint(spawnSound, Camera.main.transform.position, spawnSoundVolume);
            }
        }

        private void HandleFire()
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }

        private void HandleDie(Enemy enemy)
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }

        private void OnDestroy()
        {
            var enemyComponent = GetComponent<Enemy>();

            enemyComponent.OnSpawned -= HandleSpawn;
            enemyComponent.OnFired -= HandleFire;
            Enemy.OnDied -= HandleDie;
        }
    }
}