using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;
using System.Transactions;
using System.Net.Mail;
using System.Collections.Specialized;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KartGame.KartSystems;
using TMPro;
using Game;
using Cinemachine;

public class Kart : MonoBehaviour
{
    // 
    public string [] powerUps = {"Oil Slick", "Party Mode", "Ghost Mode"};
    public List<string> npcNames = new List<string>(){"Skippy", "Dash", "Rash", "Cash", "Soup", "Toup", "Nuns", "Vascz", "Backz"};

    public TextMeshProUGUI notification;
    Color notificationDefaultColor;
    ArcadeKart kart;

    // RACER
    public string racerName;
    Renderer kartRenderer;
    Renderer playerRenderer;
    Color kartDefaultColor;
    Color playerDefaultColor;
    GameObject headEND;
    public float powerUpTime = 5;
    public float defaultTopSpeed = 25f;
    public float defaultAcceleration = 5f;

    // GAMEMODE
    public int selectedPowerUp;
    public int checkpointCounter;
    GameObject lastCheckpoint;
    public float lastTime;
    float timer;
    public bool isPowerUpOn = false;

    public HatModel hatModel;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initializes the racer's information and registers it with the RaceController.
    /// </summary>
    /// <remarks>
    /// This function is typically used in Unity's MonoBehaviour scripts.
    /// It should be placed within the class body and executed during the object's awake phase.
    /// </remarks>
    void Awake() 
    {
        if (gameObject.CompareTag("Player"))
        {
            racerName = PlayerPrefs.GetString("PlayerUsername");
            RaceController raceController = FindObjectOfType<RaceController>();
            if (raceController != null)
            {
                raceController.racers.Insert(0, this);
                raceController.racersPositions.Insert(0, this);
            }   
        }        
    }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Initializes the racer's information, including name and visual attributes.
    /// </summary>
    /// <remarks>
    /// This function is typically used in Unity's MonoBehaviour scripts.
    /// It should be placed within the class body and executed during the object's start phase.
    /// </remarks>
    void Start()
    {
        if (gameObject.CompareTag("Player"))    // Kart Player
        {   
            racerName = PlayerPrefs.GetString("PlayerUsername");
            if (PlayerPrefs.GetInt("CarModel") == 0)
            {
                kartRenderer = gameObject.transform.Find("KartBody/Roadster_Body/Roadster_Body").GetComponent<Renderer>();
                playerRenderer = gameObject.transform.Find("KartBody/PlayerIdle/Template_Character").GetComponent<Renderer>();
                headEND = gameObject.transform.Find("KartBody/PlayerIdle/Root1/Hips/Spine1/Spine2/Neck/Head/HeadEND").gameObject;
            }
            else if (PlayerPrefs.GetInt("CarModel") == 1)
            {
                kartRenderer = gameObject.transform.Find("KartVisual/Kart/Kart_Body").GetComponent<Renderer>();
                playerRenderer = gameObject.transform.Find("KartVisual/PlayerIdle/Template_Character").GetComponent<Renderer>();
                headEND = gameObject.transform.Find("KartVisual/PlayerIdle/Root1/Hips/Spine1/Spine2/Neck/Head/HeadEND").gameObject;
            }
            string [] colorComponents;
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("KartColor")))
            {
                colorComponents = PlayerPrefs.GetString("KartColor").Replace("RGBA(", "").Replace(")", "").Split(',');
                kartRenderer.material.color = new Color(float.Parse(colorComponents[0]), float.Parse(colorComponents[1]), float.Parse(colorComponents[2]), float.Parse(colorComponents[3]));
            }
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerColor")))
            {
                colorComponents = PlayerPrefs.GetString("PlayerColor").Replace("RGBA(", "").Replace(")", "").Split(',');
                playerRenderer.material.color = new Color(float.Parse(colorComponents[0]), float.Parse(colorComponents[1]), float.Parse(colorComponents[2]), float.Parse(colorComponents[3]));
            }  
            if (PlayerPrefs.GetInt("HatModel") != null && headEND != null)
            {
                Debug.Log("INSIDE: " + headEND.ToString());
                hatModel.AddHatGame(PlayerPrefs.GetInt("HatModel"), headEND);
            }
        }
        else if (gameObject.CompareTag("KartAI"))   // Kart Agent
        {
            // Assign random name to Agent
            racerName = npcNames[Random.Range(0, 9)];       
            // Remove the name from List
            npcNames.Remove(racerName);              
            // Assign kartRenderer and playerRenderer to Agent       
            kartRenderer = gameObject.transform.Find("KartVisual/Kart/Kart_Body").GetComponent<Renderer>();     
            playerRenderer = gameObject.transform.Find("KartVisual/PlayerIdle/Template_Character").GetComponent<Renderer>();
            // Randomize kart and player color
            playerRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
            kartRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
        }
        kartDefaultColor = kartRenderer.material.color;
        playerDefaultColor = playerRenderer.material.color;
        kart = GetComponent<ArcadeKart>();
        kart.baseStats.TopSpeed = defaultTopSpeed;
        kart.baseStats.Acceleration = defaultAcceleration;
        notification.enabled = false; 
    }
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
    }

    /// <summary>
    /// Called when a collider enters the trigger zone of the current object.
    /// Handles interactions with power-ups and checkpoints.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))    // PowerUp
        {
            // Despawn caught powerUp gameObject
            Destroy(other.gameObject);      
            // Random Power Up Selection
            selectedPowerUp = Random.Range(0, 3);
            // Call to ActivatePowerUp Coroutine
            StartCoroutine(ActivatePowerUp());
        }
        else if (other.CompareTag("Checkpoint"))    // Checkpoint
        {
            // When kart starts driving back
            if (other.gameObject == lastCheckpoint) {   
                return;
            } 
            lastCheckpoint = other.gameObject;
            lastTime = timer;
            checkpointCounter++;
        }
        ArcadeKart otherKart = other.GetComponent<ArcadeKart>();
        if (otherKart != null) 
        {
            otherKart.ChangeSpeed(6);
        }
    }

    /// <summary>
    /// Coroutine to activate the selected power-up.
    /// </summary>
    /// <returns>An IEnumerator used for coroutine execution.</returns>
    IEnumerator ActivatePowerUp()
    {
        notification.enabled = true;
        notification.text = powerUps[selectedPowerUp];
        float powerUpTimer = 0;
        switch (selectedPowerUp)
        {
            case 0: // Oil Slick
                Debug.Log("No Brakes");
                isPowerUpOn = true;
                
                float tempBraking = kart.baseStats.Braking; 
                float tempSteer = kart.baseStats.Steer;
                while (powerUpTimer <= powerUpTime)
                {
                    powerUpTimer +=0.1f;
                    kart.baseStats.Braking = 0f;
                    kart.baseStats.Steer = 15f;

                    yield return new WaitForSeconds(0.1f);
                }
                kart.baseStats.Braking = tempBraking;
                kart.baseStats.Steer = tempSteer;
                isPowerUpOn = false;
                break;
            case 1: // Party Mode
                Debug.Log("Party Mode");
                isPowerUpOn = true;
                while (powerUpTimer <= powerUpTime)
                {
                    powerUpTimer +=0.1f;
                    kartRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));

                    yield return new WaitForSeconds(0.1f); 
                }
                kartRenderer.material.color = kartDefaultColor;
                isPowerUpOn = false;
                break;
            case 2: // Ghost Mode
                Debug.Log("Ghost Mode");
                isPowerUpOn = true;
                while (powerUpTimer <= powerUpTime)
                {
                    powerUpTimer +=0.1f;
                    kartRenderer.material.color = new Color(kartRenderer.material.color.r, kartRenderer.material.color.g, kartRenderer.material.color.b, 0.1f );
                    playerRenderer.material.color = new Color(playerRenderer.material.color.r,playerRenderer.material.color.g, playerRenderer.material.color.b, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                }
                kartRenderer.material.color = kartDefaultColor;
                playerRenderer.material.color = playerDefaultColor;
                isPowerUpOn = false;
                break;
            default:
                isPowerUpOn = false;
                break;
        }
        notification.text = "";
    }

    void displayDebug()
    {
        Debug.LogWarning("====================================================");
        Debug.LogWarning("Username: " + PlayerPrefs.GetString("PlayerUsername"));
        Debug.LogWarning("PColor: " + PlayerPrefs.GetString("PlayerColor"));
        Debug.LogWarning("CModel: " + PlayerPrefs.GetInt("CarModel"));
        Debug.LogWarning("CColor: " + PlayerPrefs.GetString("KartColor"));
        Debug.LogWarning("HModel: " + PlayerPrefs.GetInt("HatModel"));
        Debug.LogWarning("====================================================");
    }

}
