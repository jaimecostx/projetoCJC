using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Instantiate : MonoBehaviour
{
    public GameObject kartClassic;
    public GameObject kartRoadster;
    public CinemachineVirtualCamera camera;

    /// <summary>
    /// This method is called by Unity when the script instance is being loaded.
    /// It instantiates the kart object based on the player's car model selection and configures the camera to follow and look at the kart.
    /// </summary>
    void Start()
    {   
        GameObject kart;
        PlayerPrefs.SetString("CarModel", "1");
        Debug.Log("Instantiate" + PlayerPrefs.GetString("CarModel"));
        if (PlayerPrefs.GetString("CarModel") == "1")
        {
            // Instantiate the kartRoadster prefab at a specific position and rotation
            kart = Instantiate(kartRoadster, new Vector3(13.5f, 0.25f, 5f), Quaternion.identity);
            kart.tag = "Player";
            // Set the camera to follow the kart's transform
            camera.Follow = kart.transform;
            // Set the camera to look at the KartBouncingCapsule child object within the kart
            camera.LookAt = kart.transform.Find("KartBouncingCapsule");
            // Add the Kart Script component to the kart object
            kart.AddComponent<Kart>();
        }
        else
        {
            kart = Instantiate(kartClassic, new Vector3(13.5f, 0.25f, 5f), Quaternion.identity);
            kart.tag = "Player";
            camera.Follow = kart.transform;
            camera.LookAt = kart.transform.Find("KartBouncingCapsule");
            kart.AddComponent<Kart>();
        }

    }

    void Update()
    {
        
    }
}