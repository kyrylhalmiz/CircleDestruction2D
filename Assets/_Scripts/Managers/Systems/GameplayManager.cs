using System;
using UnityEngine;

namespace _Scripts.Managers.Systems
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;

        private int _currentLevel = 1;
        private int _points = 0;
        private int _pointsForNextLevel = 300;
        private float _speedModifier = 10f;
        private void Awake()
        {
            GameStateManager.OnNewLevel += CalculateNewLevelPoints;
            GameStateManager.OnNewLevel += GoToNextLevel;
            GameStateManager.OnNewLevel += UpdateSpeedModifier;
            GameStateManager.OnRestartGame += ResetNewLevelPoints;
            GameStateManager.OnStopGame += ResetNewLevelPoints;

            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        private void GoToNextLevel()
        {
            _currentLevel++;
        }

        public int GetNextPoints()
        {
            return _pointsForNextLevel;
        }

        public int GetPoints()
        {
            return _points;
        }

        public float GetSpeedModifier()
        {
            return _speedModifier;
        }

        public void AddPoints(int pointsAddition)
        {
            _points += pointsAddition;
            if(_points>_pointsForNextLevel)
                GameStateManager.NewLevelAction();

        }

        public void Reset()
        {
            _currentLevel = 1;
            _points = 0;
        }

        private void CalculateNewLevelPoints()
        {
            _pointsForNextLevel += _currentLevel * 300;
        }
        
        private void ResetNewLevelPoints()
        {
            _pointsForNextLevel = 300;
        }

        private void UpdateSpeedModifier()
        {
            _speedModifier *= 1.3f;
        }

        private void OnDestroy()
        {
            GameStateManager.OnNewLevel -= CalculateNewLevelPoints;
            GameStateManager.OnNewLevel -= GoToNextLevel;
            GameStateManager.OnNewLevel -= UpdateSpeedModifier;
            GameStateManager.OnRestartGame -= ResetNewLevelPoints;
            GameStateManager.OnStopGame -= ResetNewLevelPoints;
        }
    }
}
