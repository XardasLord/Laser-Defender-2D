using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyPathing : MonoBehaviour
    {
        private WaveConfig _waveConfig;
        private List<Transform> _waypoints;
        private int _currentWaypointIndex = 0;

        // Start is called before the first frame update
        void Start()
        {
            _waypoints = _waveConfig.GetWaypoints();
            transform.position = _waypoints[_currentWaypointIndex].position;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        public void SetWaveConfig(WaveConfig waveConfig)
        {
            _waveConfig = waveConfig;
        }

        private void Move()
        {
            if (_currentWaypointIndex <= _waypoints.Count - 1)
            {
                var targetPosition = _waypoints[_currentWaypointIndex].position;
                var movementThisFrame = _waveConfig.GetMoveSpeed() * Time.deltaTime;

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
