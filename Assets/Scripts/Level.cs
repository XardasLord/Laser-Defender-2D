﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private float delayInSeconds = 2f;

        private void Awake()
        {
            var playerComponent = FindObjectOfType<Player.Player>();
            if (playerComponent != null)
            {
                playerComponent.OnDied += HandlePlayerDeath;
            }
        }

        public void LoadStartMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
            FindObjectOfType<GameSession>()?.ResetGame();
        }

        public void LoadGameOver()
        {
            StartCoroutine(WaitAndLoad());
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator WaitAndLoad()
        {
            yield return new WaitForSeconds(delayInSeconds);
            SceneManager.LoadScene("Game Over");
        }

        private void HandlePlayerDeath()
        {
            LoadGameOver();
        }
    }
}
