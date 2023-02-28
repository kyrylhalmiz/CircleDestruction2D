using _Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class StateButtonsController : MonoBehaviour
    {
        [SerializeField] private Sprite stopSprite;
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Image switchButtonImage;
        
        private bool _isPlaying = true;
        
        public void RestartGame()
        {
            GameStateManager.RestartGameAction();
        }

        public void SwitchGameState()
        {
            if (_isPlaying)
            {
                StopGame();
            }
            else
            {
                StartGame();
            }

            _isPlaying = !_isPlaying;
            UpdateSwitchButtonImage();
        }

        private static void StartGame()
        {
            GameStateManager.StartGameAction();
        }

        private static void StopGame()
        {
            GameStateManager.StopGameAction();
        }

        private void UpdateSwitchButtonImage()
        {
            switchButtonImage.sprite = _isPlaying ? stopSprite : startSprite;
        }
        
    }
}
