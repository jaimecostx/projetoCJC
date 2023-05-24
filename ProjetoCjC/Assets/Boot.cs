using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CarModel", 0);
        PlayerPrefs.SetInt("HatModel", 0);
        PlayerPrefs.SetString("PlayerUsername", "");
        PlayerPrefs.SetString("PlayerColor", "");
        PlayerPrefs.SetString("KartColor", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
