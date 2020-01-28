using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private float projectileSpeed = 10f;

        private void Awake()
        {
            GetComponent<Enemy.Enemy>().OnFired += HandleFire;
        }

        private void HandleFire()
        {
            var laser = Instantiate(
                projectile,
                transform.position,
                Quaternion.identity);

            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        }

        private void OnDestroy()
        {
            GetComponent<Enemy.Enemy>().OnFired -= HandleFire;
        }
    }
}
