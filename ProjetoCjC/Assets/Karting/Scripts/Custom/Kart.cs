using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class Kart : MonoBehaviour
{
    public float powerUpTime = 5;
    public string []powerUps = {"Oil Slick", "Party Mode", "Ghost Mode"};
    // no brakes, 
    public int selectedPowerUp;
    public Renderer kartRenderer;

    public Renderer playerRenderer;
    public bool isPowerUpOn = false;
    Color kartDefaultColor;
    Color playerDefaultColor;
    ArcadeKart kart;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("KartColor"));
        string[] colorComponents = PlayerPrefs.GetString("KartColor").Replace("RGBA(", "").Replace(")", "").Split(',');

        // Parse the color components to floats
        float red = float.Parse(colorComponents[0]);
        float green = float.Parse(colorComponents[1]);
        float blue = float.Parse(colorComponents[2]);
        float alpha = float.Parse(colorComponents[3]);

        Debug.Log(red + " " + green + " " + blue + " " + alpha);

        kartRenderer.material.color = new Color(red, green, blue, alpha);
        kartDefaultColor = kartRenderer.material.color;

        Debug.Log(PlayerPrefs.GetString("PlayerColor"));
        colorComponents = PlayerPrefs.GetString("PlayerColor").Replace("RGBA(", "").Replace(")", "").Split(',');

        // Parse the color components to floats
        red = float.Parse(colorComponents[0]);
        green = float.Parse(colorComponents[1]);
        blue = float.Parse(colorComponents[2]);
        alpha = float.Parse(colorComponents[3]);

        Debug.Log(red + " " + green + " " + blue + " " + alpha);

        playerRenderer.material.color = new Color(red, green, blue, alpha);
        playerDefaultColor = playerRenderer.material.color;

        Debug.Log(kartDefaultColor);

        kart = GetComponent<ArcadeKart>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
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
    }

    IEnumerator ActivatePowerUp()
    {

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

    }

}
