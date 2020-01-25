using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float padding = 1f;

        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            SetUpMoveBoundaries();
        }

        private void Update()
        {
            Move();
        }

        private void SetUpMoveBoundaries()
        {
            var gameCamera = Camera.main;
            _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
            _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
            _yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
            _yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
        }

        private void Move()
        {
            var deltaX = _playerInput.Horizontal * Time.deltaTime * moveSpeed;
            var deltaY = _playerInput.Vertical * Time.deltaTime * moveSpeed;

            var newXPos = Mathf.Clamp(transform.position.x + deltaX, _xMin, _xMax);
            var newYPos = Mathf.Clamp(transform.position.y + deltaY, _yMin, _yMax);

            transform.position = new Vector2(newXPos, newYPos);
        }
    }
}
