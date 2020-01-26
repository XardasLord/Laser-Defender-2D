using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy")]
        [SerializeField] private float health = 100;
        [SerializeField] private int scoreValue = 50;
        [SerializeField] private bool isBoss;

        [Header("Shooting")]
        [SerializeField] private float minTimeBetweenShots = 0.5f;
        [SerializeField] private float maxTimeBetweenShots = 3f;

        public bool IsBoss => isBoss;
        public int ScoreValue => scoreValue;

        public event Action<Enemy> OnSpawned = delegate { };
        public static event Action<Enemy> OnDied = delegate { };
        public event Action OnFired = delegate { };

        private float _shotCounter;

        void Start()
        {
            OnSpawned(this);

            _shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

        void Update()
        {
            CountDownAndShoot();
        }

        private void CountDownAndShoot()
        {
            // TODO: Move this logic to separate component
            _shotCounter -= Time.deltaTime;
            if (_shotCounter <= 0f)
            {
                OnFired();

                _shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
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
            OnDied(this);

            Destroy(gameObject);
        }
    }
}
