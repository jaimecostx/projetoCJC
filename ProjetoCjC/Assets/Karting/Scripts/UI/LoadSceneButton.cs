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
            if (SceneName == "IntroMenu")
            {
                PlayerPrefs.SetInt("HatModel", 0);
                PlayerPrefs.SetInt("CarModel", 0);
                PlayerPrefs.SetString("PlayerColor", "");
                PlayerPrefs.SetString("PlayerUsername", "");
                PlayerPrefs.SetString("KartColor", "");
            }
            SceneManager.LoadSceneAsync(SceneName);
            Debug.Log("===== " + SceneName + " =====");
            displayDebug();
        }

        public void OnButtonClick()
        {
            if (string.IsNullOrEmpty(username.text))
            {
                Debug.Log("ManRacer");
                PlayerPrefs.SetString("PlayerUsername", "ManRacer");
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log(username.text);
                PlayerPrefs.SetString("PlayerUsername", username.text);
                PlayerPrefs.Save();
            }            
            displayDebug();
        }

        void displayDebug()
        {
            Debug.Log("Username: " + PlayerPrefs.GetString("PlayerUsername"));
            Debug.Log("PColor: " + PlayerPrefs.GetString("PlayerColor"));
            Debug.Log("CModel: " + PlayerPrefs.GetInt("CarModel"));
            Debug.Log("CColor: " + PlayerPrefs.GetString("KartColor"));
            Debug.Log("HModel: " + PlayerPrefs.GetInt("HatModel"));
        }
    }
}
