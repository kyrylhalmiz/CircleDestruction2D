using System;
using _Scripts.Managers;
using _Scripts.Managers.Systems;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class SetInfoTextController : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;
        private float _time;
        private bool _timeStopped = false;
        public void Awake()
        {
            _tmp = gameObject.GetComponent<TextMeshProUGUI>();
            GameStateManager.OnRestartGame += ResetText;
            GameStateManager.OnStopGame += ResetText;
            GameStateManager.OnStopGame += SwitchTime;
            GameStateManager.OnStartGame += SwitchTime;
        }

        public void Update()
        {
            if(!_timeStopped)
                _time += Time.deltaTime;
            _tmp.text = $"Time: {(int)_time}s. Points: {GameplayManager.Instance.GetPoints()}";
        }

        private void ResetText()
        {
            GameplayManager.Instance.Reset();
            _time = 0;
        }

        private void SwitchTime()
        {
            _timeStopped = !_timeStopped;
        }

        private void OnDisable()
        {
            GameStateManager.OnRestartGame -= ResetText;
            GameStateManager.OnStopGame -= ResetText;
            GameStateManager.OnStopGame -= SwitchTime;
            GameStateManager.OnStartGame -= SwitchTime;
        }
    }
}
