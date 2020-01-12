using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyPathing : MonoBehaviour
    {
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private float moveSpeed = 2f;

        private int _waipointIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            transform.position = waypoints[_waipointIndex].position;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_waipointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[_waipointIndex].position;
                var movementThisFrame = moveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

                if (transform.position == waypoints[_waipointIndex].position)
                {
                    _waipointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
