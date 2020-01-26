using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyParticles : MonoBehaviour
    {
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private float durationOfExplosion = 1f;

        private void Awake()
        {
            Enemy.OnDied += HandleDie;
        }

        private void HandleDie(Enemy enemy)
        {
            var explosion = Instantiate(deathVFX, enemy.transform.position, enemy.transform.rotation);
            Destroy(explosion, durationOfExplosion);
        }
    }
}
