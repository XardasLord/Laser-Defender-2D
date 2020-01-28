using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int health = 200;

        public event Action OnDied = delegate { };
        public event Action OnHit = delegate { };

        public int Health => health;

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

            OnHit();

            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDied();

            Destroy(gameObject);
        }
    }
}
