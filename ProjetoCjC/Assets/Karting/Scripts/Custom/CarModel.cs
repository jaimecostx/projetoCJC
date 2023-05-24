using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{

public class CarModel : MonoBehaviour
{
    public GameObject[] carPrefabs;
    // 0 - Roadster 1 - Classic
    public int currentCarIndex = 0;
    // Start is called before the first frame update
    public GameObject carModel;
    void Start()
    {
        carPrefabs[currentCarIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        // Disable the current car
        carPrefabs[currentCarIndex].SetActive(false);

        // Increment the current car index
        currentCarIndex = (currentCarIndex + 1) % carPrefabs.Length;

        // Enable the next car
        carPrefabs[currentCarIndex].SetActive(true);
        carModel = carPrefabs[currentCarIndex];

        PlayerPrefs.SetInt("CarModel", currentCarIndex);
        PlayerPrefs.Save();
    }
}

}