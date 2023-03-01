using _Scripts.Managers;
using _Scripts.Managers.Systems;
using UnityEngine;

namespace _Scripts.Controllers.Circle
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

            SetColor();
        }

        private void SetColor()
        {
            _spriteRenderer.color = _scale switch
            {
                < 1.5f => TextureManager.Instance.GetTextures()[0].GetPixel(0, 0),
                >= 1.5f and < 3f => TextureManager.Instance.GetTextures()[1].GetPixel(0, 0),
                >= 3f and < 4f => TextureManager.Instance.GetTextures()[2].GetPixel(0, 0),
                >= 4f and <= 5f => TextureManager.Instance.GetTextures()[3].GetPixel(0, 0),
                _ => _spriteRenderer.color
            };
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
