using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private float health = 100;
        [SerializeField] private int scoreValue = 50;

        [Header("Shooting")]
        [SerializeField] private float shotCounter;
        [SerializeField] private float minTimeBetweenShots = 0.2f;
        [SerializeField] private float maxTimeBetweenShots = 3f;

        [Header("Projectile")]
        [SerializeField] private GameObject projectile;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private GameObject deathVFX;
        [SerializeField] private float durationOfExplosion = 1f;

        [Header("Sound effects")]
        [SerializeField] private AudioClip deathSound;
        [SerializeField] [Range(0,1)] private float deathSoundVolume = 0.75f;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] [Range(0, 1)] private float shootSoundVolume = 0.75f;

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
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
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
            FindObjectOfType<GameSession>().AddScore(scoreValue);
            Destroy(gameObject);
            var explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }
    }
}
