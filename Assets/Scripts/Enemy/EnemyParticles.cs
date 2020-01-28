using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyParticles : MonoBehaviour
    {
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private float durationOfExplosion = 1f;

        private void Awake()
        {
            GetComponent<Enemy>().OnDied += HandleDie;
        }

        private void HandleDie(Enemy enemy)
        {
            var explosion = Instantiate(deathVFX, enemy.transform.position, enemy.transform.rotation);
            Destroy(explosion, durationOfExplosion);
        }
    }
}
