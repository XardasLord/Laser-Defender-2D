using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public float Vertical { get; private set; }
        public float Horizontal { get; private set; }

        public event Action OnFired = delegate { };

        private void Update()
        {
            Vertical = Input.GetAxis("Vertical");
            Horizontal = Input.GetAxis("Horizontal");

            if (Input.GetButton("Fire1"))
            {
                OnFired();
            }
        }
    }
}
