using UnityEngine;

namespace _Scripts.Managers.Systems
{

        public class AudioManager : MonoBehaviour
        {
            public static AudioManager Instance;

            private AudioSource _audioPlayer;

            private void Awake()
            {
                _audioPlayer = GetComponent<AudioSource>();

                if (Instance != null)
                    Destroy(gameObject);
                else
                    Instance = this;
            }

            public void PlaySound(AudioClip soundToPlay)
            {
                _audioPlayer.PlayOneShot(soundToPlay);
            }
        }
}   

