using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float health = 100;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageDealer = other.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }

        private void ProcessHit(DamageDealer damageDealer)
        {
            damageDealer.Hit();
            health -= damageDealer.Damage;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
