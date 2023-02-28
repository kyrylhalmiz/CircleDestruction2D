using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.AssetManagement
{
    public class BackgroundLoader : MonoBehaviour
    {
        [SerializeField] private bool clearCache = false;
        [SerializeField] private Slider  progressSlider;
        [SerializeField] private Button loadSceneButton;

#if UNITY_EDITOR || PLATFORM_STANDALONE
        private const string MUri = "https://drive.google.com/uc?export=download&id=1urfAm2M1OUPU_2TlbCQhU8-S61aDuJb8";
#elif UNITY_ANDROID
        private const string MUri = "https://drive.google.com/uc?export=download&id=10X5bodVMVdVGCkdvRDMekN9o6dEiqWVv";
#elif UNITY_IOS
        private const string MUri = "https://drive.google.com/uc?export=download&id=1_JHDi01CGMhcO7lA0GhXKA2OClVElkFc";
#endif

        private static Sprite[] _receivedBackgrounds;
        private AssetBundle _mBundle = null;
        private void Awake()
        {
            Caching.compressionEnabled = false;

            if (clearCache)
                Caching.ClearCache();
        }

        private void Start()
        {
            StartCoroutine(DownloadAndSetBackgrounds());
        }

        private IEnumerator DownloadAndSetBackgrounds()
        {
            yield return GetBundle();

            if (!_mBundle)
            {
                Debug.Log("Bundle failed to load");
                yield break;
            }
            
            _receivedBackgrounds = _mBundle.LoadAllAssets<Sprite>();
            loadSceneButton.interactable = true;
        }
        private IEnumerator GetBundle()
        {
            WWW request = WWW.LoadFromCacheOrDownload(MUri,0);

            while (!request.isDone)
            {
                Debug.Log(request.progress);
                progressSlider.value = request.progress;
                yield return null;
            }

            if (request.error == null)
            {
                _mBundle = request.assetBundle;
                Debug.Log("Success");
            }
            else
            {
                Debug.Log(request.error);
            }
            progressSlider.value = 1;
        }

        public static Sprite[] GetAssetBackgrounds()
        {
            return _receivedBackgrounds;
        }
    }
}
