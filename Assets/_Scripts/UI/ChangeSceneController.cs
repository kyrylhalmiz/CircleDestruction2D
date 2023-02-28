using _Scripts.Managers.Systems;
using UnityEngine;

namespace _Scripts.UI
{
    public class ChangeSceneController : MonoBehaviour
    {

        public void ChangeScene(string sceneName)
        {
            LevelManager.LoadScene(sceneName);
        }
    }
}
