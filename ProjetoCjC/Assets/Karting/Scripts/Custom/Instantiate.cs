using System.Net.Cache;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using Game;
public class Instantiate : MonoBehaviour
{
    public HatModel hatScript;
    public GameObject kartClassic;
    public GameObject kartRoadster;
    public GameObject powerUpFx;
    public TextMeshProUGUI notification;
    Kart player;
    public CinemachineVirtualCamera camera;

    /// <summary>
    /// This method is called by Unity when the script instance is being loaded.
    /// It instantiates the kart object based on the player's car model selection and configures the camera to follow and look at the kart.
    /// </summary>
    void Awake()
    {   
        GameObject kart;
        Debug.Log("Instantiate" + PlayerPrefs.GetInt("CarModel"));
        if (PlayerPrefs.GetInt("CarModel") == 0)
        {
            // Instantiate the kartRoadster prefab at a specific position and rotation
            kart = Instantiate(kartRoadster, new Vector3(13.5f, 0.25f, 5f), Quaternion.identity);
            // Set the scale to 0.88
            kart.transform.localScale = new Vector3(0.88f, 0.88f, 0.88f); 
            kart.tag = "Player";
            kart.layer = 13;
            // Set the camera to follow the kart's transform
            camera.Follow = kart.transform;
            // Set the camera to look at the KartBouncingCapsule child object within the kart
            camera.LookAt = kart.transform.Find("KartBouncingCapsule");
            // Add the Kart Script component to the kart object
            player = kart.AddComponent<Kart>();
            player.notification = notification;
            player.hatModel = hatScript;
            player.powerupFx = powerUpFx;
        }
        else
        {
            kart = Instantiate(kartClassic, new Vector3(13.5f, 0.25f, 5f), Quaternion.identity);
            kart.tag = "Player";
            kart.layer = 13;
            camera.Follow = kart.transform;
            camera.LookAt = kart.transform.Find("KartBouncingCapsule");
            player = kart.AddComponent<Kart>();
            player.notification = notification;
            player.hatModel = hatScript;
            player.powerupFx = powerUpFx;
        }
    }

    void Update()
    {
        
    }

}
