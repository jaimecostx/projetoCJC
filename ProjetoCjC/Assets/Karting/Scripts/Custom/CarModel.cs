using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{

public class CarModel : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public int currentCarIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentCarIndex = 0;
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

        Debug.Log("CarModel" + currentCarIndex);
        PlayerPrefs.SetString("CarModel", currentCarIndex.ToString());
        
    }
}

}