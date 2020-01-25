using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class HealthDisplay : MonoBehaviour
    {
        private Text _healthText;
        private Player _player;

        private void Awake()
        {
            _healthText = GetComponent<Text>();
            _player = FindObjectOfType<Player>();

            _healthText.text = _player.Health.ToString();

            _player.OnHit += HandleOnHit;
        }

        private void HandleOnHit()
        {
            _healthText.text = _player.Health.ToString();
        }
    }
}
