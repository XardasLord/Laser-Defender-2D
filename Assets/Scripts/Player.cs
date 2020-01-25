using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerProjectileLauncher))]
    public class Player : MonoBehaviour
    {
        [SerializeField] 
        private int health = 200;

        private PlayerProjectileLauncher _playerProjectileLauncher;

        public event Action OnFire = delegate { };
        public event Action OnDie = delegate { };

        public int Health => health;

        private void Awake()
        {
            _playerProjectileLauncher = GetComponent<PlayerProjectileLauncher>();
        }

        private void Update()
        {
            Fire();
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

        private void Fire()
        {
            // Maybe move this if logic to the Input handle class like 'PlayerInput'?
            if (Input.GetButton("Fire1") && _playerProjectileLauncher.CanFire())
            {
                OnFire();
            }
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            // Maybe raise the even OnHit()?
            damageDealer.Hit();
            health -= damageDealer.Damage;

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDie();

            health = 0;
            Destroy(gameObject);
        }
    }
}
