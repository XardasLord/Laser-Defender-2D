using UnityEngine;

namespace Assets.Scripts
{
    public class MusicPlayer : MonoBehaviour
    {
        private void Awake()
        {
            SetUpSingleton();
        }

        private void SetUpSingleton()
        {
            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
