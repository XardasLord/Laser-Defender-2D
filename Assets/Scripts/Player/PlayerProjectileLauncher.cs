using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float projectileFiringPeriod = 0.1f;

        private float _fireCooldown;

        public event Action OnFireStarted = delegate { };

        private void Awake()
        {
            GetComponent<PlayerInput>().OnFired += HandleFire;
        }

        private void HandleFire()
        {
            if (!CanFire())
            {
                return;
            }
            
            var laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            _fireCooldown = Time.time + projectileFiringPeriod;

            OnFireStarted();
        }

        private bool CanFire()
        {
            return Time.time > _fireCooldown;
        }
    }
}
