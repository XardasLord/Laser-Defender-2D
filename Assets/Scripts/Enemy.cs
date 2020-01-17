using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float health = 100;
        [SerializeField] private float shotCounter;
        [SerializeField] private float minTimeBetweenShots = 0.2f;
        [SerializeField] private float maxTimeBetweenShots = 3f;
        [SerializeField] private GameObject projectile;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private float durationOfExplosion = 1f;

        // Start is called before the first frame update
        void Start()
        {
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

        // Update is called once per frame
        void Update()
        {
            CountDownAndShoot();
        }

        private void CountDownAndShoot()
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                Fire();
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }

        private void Fire()
        {
            var laser = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageDealer = other.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer)
            {
                return;
            }

            ProcessHit(damageDealer);
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            damageDealer.Hit();
            health -= damageDealer.Damage;

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
            var explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
        }
    }
}
