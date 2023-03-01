using UnityEngine;

namespace _Scripts.Circle
{
    public class CircleDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag($"Circle"))
                Destroy(other.gameObject);
        }
        
    }
}
