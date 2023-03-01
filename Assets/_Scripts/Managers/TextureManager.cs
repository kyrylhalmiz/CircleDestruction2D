using _Scripts.Managers.Systems;
using UnityEngine;

namespace _Scripts.Managers
{
    public class TextureManager : MonoBehaviour
    {
        public static TextureManager Instance;
        public Texture2D[] _textures;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                GenerateTextures();
                GameStateManager.OnNewLevel += RegenerateTextures;
            }
        }

        private void GenerateTextures()
        {
            _textures = new Texture2D[4];

            for (int i = 0; i < _textures.Length; i++)
            {
                int size = (int)Mathf.Pow(2, i + 5); // calculate size based on index
                Texture2D texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        texture.SetPixel(x, y, randomColor);
                    }
                }
                texture.Apply();
                _textures[i] = texture;
            }
        }

        public Texture2D[] GetTextures()
        {
            return _textures;
        }

        private void RegenerateTextures()
        {
            for (int i = 0; i < _textures.Length; i++)
            {
                Texture2D oldTexture = _textures[i];
                int size = (int)Mathf.Pow(2, i + 5); // calculate size based on index
                Texture2D newTexture = new Texture2D(size, size, TextureFormat.RGBA32, false);
                Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        newTexture.SetPixel(x, y, randomColor);
                    }
                }
                newTexture.Apply();
                _textures[i] = newTexture;
                Destroy(oldTexture);
            }
        }

        void OnDestroy()
        {
            // Dispose of old textures
            foreach (Texture2D texture in _textures)
            {
                Destroy(texture);
            }
        }
    }
}
