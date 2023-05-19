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
    public string [] powerUps = {"Oil Slick", "Party Mode", "Ghost Mode"};
    public string [] npcNames = {"Skippy", "Dash", "Rash", "Cash", "Soup", "Toup", "Nuns", "Vascz", "Backz"};

    public TextMeshProUGUI notification;
    public string racerName;
    public float powerUpTime = 5;

    public int selectedPowerUp;
    Renderer kartRenderer;
    Renderer playerRenderer;
    public int checkpointCounter;

    public bool isPowerUpOn = false;

    public float lastTime;
    float timer;
    Color kartDefaultColor;
    Color playerDefaultColor;
    Color notificationDefaultColor;
    ArcadeKart kart;

    public CarModel carSwitcher;
    

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            racerName = PlayerPrefs.GetString("PlayerUsername");
            if (PlayerPrefs.GetInt("CarModel") == 0)
            {
                kartRenderer = gameObject.transform.Find("KartBody/Roadster_Body/Roadster_Body").GetComponent<Renderer>();
                playerRenderer = gameObject.transform.Find("KartBody/PlayerIdle/Template_Character").GetComponent<Renderer>();
                
            }
            else if (PlayerPrefs.GetInt("CarModel") == 1)
            {
                kartRenderer = gameObject.transform.Find("KartVisual/Kart/Kart_Body").GetComponent<Renderer>();
                playerRenderer = gameObject.transform.Find("KartVisual/PlayerIdle/Template_Character").GetComponent<Renderer>();
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
        }
        else if (gameObject.CompareTag("KartAI"))
        {
            racerName = npcNames[Random.Range(0, 9)];
            kartRenderer = gameObject.transform.Find("KartVisual/Kart/Kart_Body").GetComponent<Renderer>();
            playerRenderer = gameObject.transform.Find("KartVisual/PlayerIdle/Template_Character").GetComponent<Renderer>();
            playerRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
            kartRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
        }
        kartDefaultColor = kartRenderer.material.color;
        playerDefaultColor = playerRenderer.material.color;
        kart = GetComponent<ArcadeKart>();
    }
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            selectedPowerUp = Random.Range(0, 3);
            Debug.Log("selectPowerup " + selectedPowerUp);
            StartCoroutine(ActivatePowerUp());
        }
        else if (other.CompareTag("Checkpoint"))
        {
            lastTime = timer;
            checkpointCounter++;
        }
    }

    IEnumerator ActivatePowerUp()
    {
        //notification.enabled = true;
        //notification.text = powerUps[selectedPowerUp];
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
        //notification.text = "";
        //notification.enabled = true;
    }

}
