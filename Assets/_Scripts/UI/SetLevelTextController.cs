using _Scripts.Managers.Systems;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class SetLevelTextController : MonoBehaviour
    {
        private TextMeshProUGUI _tmp;

        public void Awake()
        {
            _tmp = gameObject.GetComponent<TextMeshProUGUI>();
        }
        public void Update()
        {
            _tmp.text = $"Level: {GameplayManager.Instance.GetLevel()}. Next lvl points: {GameplayManager.Instance.GetNextPoints()}";
        }
    }
}
