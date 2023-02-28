using _Scripts.Managers;
using _Scripts.Managers.Systems;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Circle
{
    public class Circle : MonoBehaviour
    {

        [SerializeField] private AudioClip popCircleSound;

        private float _scale;
        private Camera _camera;
        private SpriteRenderer _spriteRenderer;
        private Transform _transform;
        private int _screenHeight;
        private int _screenWidth;

        private void Awake()
        {
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = transform;
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
        }

        private void Start()
        {
            CircleSetup();
        }

        private void CircleSetup()
        {
            _scale = Random.Range(0.5f, 5f);
            _transform.localScale = new Vector3(_scale, _scale);
            
            var buffer = _screenHeight * _scale / 10;

            if (_camera != null)
            {
                var inWorldStartingPosition = _camera.ScreenToWorldPoint(new Vector3(Random.Range(buffer, _screenWidth-buffer), _screenHeight, 10));
                
                inWorldStartingPosition.y += _transform.localScale.y / 2;
                _transform.position = inWorldStartingPosition;
            }

            _spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.2f, 1f);
        }
        
        private void Update()
        {
            _transform.position += - _transform.up * (Time.deltaTime * GameplayManager.Instance.GetSpeedModifier()) / _scale;
            
            if (Input.touchCount > 0)
            {
                for (var i = 0; i < Input.touchCount; i++)
                {
                    var touch = Input.GetTouch(i);
                    
                    if (touch.phase == TouchPhase.Began && IsTouchingObject(touch.position))
                    {
                        HandleTap();
                    }
                }
            }
            
            if (Input.GetMouseButtonDown(0) && IsTouchingObject(Input.mousePosition))
            {
                HandleTap();
            }
        }
        
        private bool IsTouchingObject(Vector3 screenPos)
        {
            Vector2 worldPos = _camera.ScreenToWorldPoint(screenPos);
            var hit = Physics2D.Raycast(worldPos, Vector2.zero);
            
            return hit.transform == transform;
        }
        
        private void HandleTap()
        {
            GameplayManager.Instance.AddPoints(100/((int)_transform.localScale.y+1));
            AudioManager.Instance.PlaySound(popCircleSound);
            Destroy(gameObject);
        }

        private void OnBecameInvisible() {
            Destroy(gameObject);
        }
        
        
    }
}
