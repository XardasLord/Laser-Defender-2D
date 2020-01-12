using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyPathing : MonoBehaviour
    {
        [SerializeField] private WaveConfig waveConfig;
        [SerializeField] private float moveSpeed = 2f;

        private List<Transform> _waypoints;
        private int _currentWaypointIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            _waypoints = waveConfig.GetWaypoints();
            transform.position = _waypoints[_currentWaypointIndex].position;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_currentWaypointIndex <= _waypoints.Count - 1)
            {
                var targetPosition = _waypoints[_currentWaypointIndex].position;
                var movementThisFrame = moveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

                if (transform.position == _waypoints[_currentWaypointIndex].position)
                {
                    _currentWaypointIndex++;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
