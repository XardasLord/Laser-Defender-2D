﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 8f;
        [SerializeField] private float padding = 1f;
        [SerializeField] private GameObject laserPrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float projectileFiringPeriod = 0.1f;

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
    }
}
