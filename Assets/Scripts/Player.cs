using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float padding = 1f;
        [SerializeField] private int health = 200;

        [Header("Projectile")]
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float projectileFiringPeriod = 0.1f;

        [Header("Sound effects")]
        [SerializeField] private AudioClip deathSound;
        [SerializeField] [Range(0, 1)] private float deathSoundVolume = 0.75f;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] [Range(0, 1)] private float shootSoundVolume = 0.75f;

        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        private Coroutine _firingCoroutine;

        // Start is called before the first frame update
        private void Start()
        {
            SetUpMoveBoundaries();
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
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

        private void Move()
        {
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
            var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);

            transform.position = new Vector2(newXPos, newYPos);
        }

        private void Fire()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _firingCoroutine = StartCoroutine(FireContinuously());
            }

            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(_firingCoroutine);
            }
        }

        IEnumerator FireContinuously()
        {
            while (true)
            {
                var laser = Instantiate(
                    laserPrefab,
                    transform.position,
                    Quaternion.identity);

                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);

                yield return new WaitForSeconds(projectileFiringPeriod);
            }
        }

        private void SetUpMoveBoundaries()
        {
            var gameCamera = Camera.main;
            _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            damageDealer.Hit();
            health -= damageDealer.Damage;

            if (health <= 0)
            {
                health = 0;
                Die();
            }
        }

        private void Die()
        {
            FindObjectOfType<Level>().LoadGameOver();
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }

        public int GetHealth()
        {
            return health;
        }
    }
}
