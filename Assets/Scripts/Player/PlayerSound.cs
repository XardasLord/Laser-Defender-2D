using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip shootSound;

        [SerializeField]
        [Range(0, 1)] 
        private float deathSoundVolume = 0.75f;

        [SerializeField] 
        [Range(0, 1)] 
        private float shootSoundVolume = 0.75f;

        private void Awake()
        {
            GetComponent<Player>().OnDied += HandlePlayerDeath;
            GetComponent<PlayerProjectileLauncher>().OnFireStarted += HandleFire;
        }

        private void HandlePlayerDeath()
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }

        private void HandleFire()
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }
    }
}
