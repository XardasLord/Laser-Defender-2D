using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float projectileFiringPeriod = 0.1f;

        private float _fireCooldown;

        public event Action OnStartFire = delegate { };

        private void Start()
        {
            GetComponent<Player>().OnFire += HandleFire;
        }

        private void OnDestroy()
        {
            GetComponent<Player>().OnFire -= HandleFire;
        }

        private void HandleFire()
        {
            var laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            _fireCooldown = Time.time + projectileFiringPeriod;

            OnStartFire();
        }

        public bool CanFire()
        {
            return Time.time > _fireCooldown;
        }
    }
}
