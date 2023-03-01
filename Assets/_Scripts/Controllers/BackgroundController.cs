using System;
using _Scripts.AssetManagement;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.UI
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Sprite[] backgroundSprites;
        
        private int _currentSpriteIndex=0;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GameStateManager.OnNewLevel += SetBackgroundSprite;
            GameStateManager.OnRestartGame += ResetBackground;
            GameStateManager.OnStopGame += ResetBackground;
        }

        private void Start()
        {
            backgroundSprites = BackgroundLoader.GetAssetBackgrounds();
            SetBackgroundSprite();
        }
        

        private void SetBackgroundSprite()
        {
            _spriteRenderer.sprite = backgroundSprites[_currentSpriteIndex];
            _currentSpriteIndex = (_currentSpriteIndex + 1) % backgroundSprites.Length;
        }

        private void ResetBackground()
        {
            _currentSpriteIndex = 0;
            _spriteRenderer.sprite = backgroundSprites[_currentSpriteIndex];
            _currentSpriteIndex++;
        }
    }
}
