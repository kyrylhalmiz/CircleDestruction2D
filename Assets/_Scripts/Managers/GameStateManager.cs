using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        public static event Action OnRestartGame;
        public static event Action OnStopGame;
        public static event Action OnStartGame;
        public static event Action OnNewLevel;

        public static void RestartGameAction()
        {
            OnRestartGame?.Invoke();
        }
        public static void StopGameAction()
        {
            OnStopGame?.Invoke();
        }
        public static void StartGameAction()
        {
            OnStartGame?.Invoke();
        }
        public static void NewLevelAction()
        {
            OnNewLevel?.Invoke();
        }

    }
}
