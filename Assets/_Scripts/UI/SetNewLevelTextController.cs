using System;
using System.Collections;
using _Scripts.Managers;
using _Scripts.Managers.Systems;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class SetNewLevelTextController : MonoBehaviour
    {
        [SerializeField] private GameObject newLevelTextObject;
        private TextMeshProUGUI _tmp;
        public void Awake()
        {
            _tmp = newLevelTextObject.GetComponent<TextMeshProUGUI>();
            GameStateManager.OnNewLevel += StartShowingGameObjectForFiveSeconds;
            GameStateManager.OnNewLevel += UpdateNewLevelText;
        }
        private IEnumerator ShowGameObjectForFiveSeconds()
        {
            newLevelTextObject.SetActive(true);
            
            yield return new WaitForSeconds(1f);
            
            newLevelTextObject.SetActive(false);
        }
        
        private void StartShowingGameObjectForFiveSeconds()
        {
            StartCoroutine(ShowGameObjectForFiveSeconds());
        }

        private void UpdateNewLevelText()
        {
            _tmp.text = $"Welcome to Level{GameplayManager.Instance.GetLevel()}";
        }

        private void OnDestroy()
        {
            GameStateManager.OnNewLevel -= StartShowingGameObjectForFiveSeconds;
            GameStateManager.OnNewLevel -= UpdateNewLevelText;
        }
        
    }
}
