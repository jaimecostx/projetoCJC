using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Race : MonoBehaviour
{
    void Start()
    {
        Debug.Log("===== RACE =====");
        if (PlayerPrefs.HasKey("PlayerUsername"))
        {
            Debug.Log("Username: " + PlayerPrefs.GetString("PlayerUsername"));
            Debug.Log("PColor: " + PlayerPrefs.GetString("PlayerColor"));
            Debug.Log("CModel: " + PlayerPrefs.GetInt("CarModel"));
            Debug.Log("CColor: " + PlayerPrefs.GetString("KartColor"));
            Debug.Log("HModel: " + PlayerPrefs.GetInt("HatModel"));
        }
    }
    
}