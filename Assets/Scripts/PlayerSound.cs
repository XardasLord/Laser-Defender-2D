using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField]
        private AudioClip deathSound;

        [SerializeField]
        [Range(0, 1)]
        private float deathSoundVolume = 0.75f;

        [SerializeField] 
        private AudioClip shootSound;

        [SerializeField] [Range(0, 1)] 
        private float shootSoundVolume = 0.75f;

        private void Start()
        {
            GetComponent<Player>().OnDie += HandlePlayerDeath;
            GetComponent<PlayerProjectileLauncher>().OnStartFire += HandleFireStart;
        }

        private void OnDestroy()
        {
            GetComponent<Player>().OnDie -= HandlePlayerDeath;
            GetComponent<PlayerProjectileLauncher>().OnStartFire -= HandleFireStart;
        }

        private void HandlePlayerDeath()
        {
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        }

        private void HandleFireStart()
        {
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
        }
    }
}
