using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        public InputField username;

        public void LoadTargetScene() 
        {
            SceneManager.LoadSceneAsync(SceneName);
        }

        public void OnButtonClick()
        {
            Debug.Log(username.text);
        }
    }
}
