using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ScoreDisplay : MonoBehaviour
    {
        private Text _scoreText;
        private GameSession _gameSession;

        private void Start()
        {
            _scoreText = GetComponent<Text>();
            _gameSession = FindObjectOfType<GameSession>();
        }

        private void Update()
        {
            _scoreText.text = _gameSession.GetScore().ToString();
        }
    }
}
