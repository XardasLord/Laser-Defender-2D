using UnityEngine;

namespace Assets.Scripts
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int damage = 100;

        public int Damage => damage;

        public void Hit()
        {
            Destroy(gameObject);
        }
    }
}
