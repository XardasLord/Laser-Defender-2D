using UnityEngine;

namespace Assets.Scripts
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField] private float speedOfSpin = 1f;

        void Update()
        {
            transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
        }
    }
}
