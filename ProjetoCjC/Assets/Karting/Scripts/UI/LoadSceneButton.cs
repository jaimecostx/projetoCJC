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
            if (string.IsNullOrEmpty(username.text))
            {
                Debug.Log("ManRacer");
                PlayerPrefs.SetString("PlayerUsername", "ManRacer");
            }
            else
            {
                Debug.Log(username.text);
                PlayerPrefs.SetString("PlayerUsername", username.text);
            }

            // PlayerPrefs.Save();
            
            Debug.Log("Username: " + PlayerPrefs.GetString("PlayerUsername"));
            Debug.Log("PColor: " + PlayerPrefs.GetString("PlayerColor"));
            Debug.Log("CModel: " + PlayerPrefs.GetInt("CarModel"));
            Debug.Log("CColor: " + PlayerPrefs.GetString("KartColor"));
            Debug.Log("HModel: " + PlayerPrefs.GetInt("HatModel"));
            
        }
    }
}
