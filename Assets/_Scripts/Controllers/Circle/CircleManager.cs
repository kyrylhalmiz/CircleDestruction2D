using System.Collections;
using _Scripts.Managers;
using _Scripts.Managers.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Circle
{
   public class CircleManager : MonoBehaviour
   {
      [SerializeField] private GameObject circle;
      
      [SerializeField] private float minSpawnDelay = 1f;
      [SerializeField] private float maxSpawnDelay = 2f;
      [SerializeField] private AudioClip newLevelSound;
      
      private float _nextSpawnTime;
      private bool _stopSpawn = false;

      private void Awake()
      {
         GameStateManager.OnRestartGame += DestroyAllCircles;
         GameStateManager.OnStopGame += DestroyAllCircles;
         GameStateManager.OnStopGame += ToggleSpawn;
         GameStateManager.OnStartGame += ToggleSpawn;
         GameStateManager.OnStartGame += OnStartSpawn;
         GameStateManager.OnNewLevel += UpdateSpawnDelays;
         GameStateManager.OnNewLevel += PlayNewLevelSound;
      }

      private void Start()
      {
         OnStartSpawn();
      }

      private IEnumerator SpawnCirclesAtRandom() {
         while(!_stopSpawn) {
            Instantiate(circle);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
         }
      }

      private void ToggleSpawn()
      {
         _stopSpawn = !_stopSpawn;
      }

      private void OnStartSpawn()
      {
         StartCoroutine(SpawnCirclesAtRandom());
      }

      private static void DestroyAllCircles()
      {
         var circles = GameObject.FindGameObjectsWithTag("Circle");
         foreach (var c in circles)
            Destroy(c);
      }

      private void UpdateSpawnDelays()
      {
         if (minSpawnDelay > 0.3f)
            minSpawnDelay -= 0.1f;
         if (maxSpawnDelay > 0.5f)
            maxSpawnDelay -= 0.15f;
      }

      private void PlayNewLevelSound()
      {
         AudioManager.Instance.PlaySound(newLevelSound);
      }
      
      
   }
}
