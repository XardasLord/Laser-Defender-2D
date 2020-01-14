using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WaveConfig> waveConfigs;
        [SerializeField] private int startingWave = 0;
        [SerializeField] private bool looping = false;

        // Start is called before the first frame update
        IEnumerator Start()
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            } while (looping);
        }

        private IEnumerator SpawnAllWaves()
        {
            for (var i = startingWave; i < waveConfigs.Count; i++)
            {
                var currentWave = waveConfigs[i];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            }
        }

        private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
        {
            for (var i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
            {
                var newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWaypoints()[0].transform.position,
                    Quaternion.identity);

                newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

                yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
            }
        }
    }
}
