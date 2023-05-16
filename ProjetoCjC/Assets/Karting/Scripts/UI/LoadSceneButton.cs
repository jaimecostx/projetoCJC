using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace KartGame.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;

        public TMP_InputField username;

        public void LoadTargetScene() 
        {
            SceneManager.LoadSceneAsync(SceneName);
        }

        public void OnButtonClick()
        {
            if (username.text == null)
            {
                Debug.Log("ManRacer");
                PlayerPrefs.SetString("PlayerUsername", "ManRacer");
            }
            else
            {
                Debug.Log(username.text);
                PlayerPrefs.SetString("PlayerUsername", username.text);
            }
            
        }
    }
}
