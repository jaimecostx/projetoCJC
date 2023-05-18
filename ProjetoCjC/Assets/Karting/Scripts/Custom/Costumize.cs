
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

public class Costumize : MonoBehaviour 
{
    public CarModel carSwitcher;

    Renderer kartRenderer;
    Renderer playerRenderer;

    public Renderer kartClassicRenderer;
    public Renderer playerClassicRenderer;

    public Renderer kartRoadsterRenderer;
    public Renderer playerRoadsterRenderer;

    Color kartDefaultColor;
    Color playerDefaultColor;

    // Start is called before the first frame update
    // Load already saved character costumization
    void Start() 
    {
        if (carSwitcher.currentCarIndex == 0)
        {
            kartRenderer = kartClassicRenderer;
            playerRenderer = playerClassicRenderer;
        }
        else if (carSwitcher.currentCarIndex == 1)
        {
            kartRenderer = kartRoadsterRenderer;
            playerRenderer = playerRoadsterRenderer;
        }

        kartDefaultColor = kartRenderer.material.color;
        playerDefaultColor = playerRenderer.material.color;

       if (PlayerPrefs.HasKey("KartColor") && PlayerPrefs.HasKey("PlayerColor"))
       {
            string[] colorComponents = PlayerPrefs.GetString("KartColor").Replace("RGBA(", "").Replace(")", "").Split(',');

            // Parse the color components to floats
            float red = float.Parse(colorComponents[0]);
            float green = float.Parse(colorComponents[1]);
            float blue = float.Parse(colorComponents[2]);
            float alpha = float.Parse(colorComponents[3]);

            kartRenderer.material.color = new Color(red, green, blue, alpha);

            colorComponents = PlayerPrefs.GetString("PlayerColor").Replace("RGBA(", "").Replace(")", "").Split(',');

            // Parse the color components to floats
            red = float.Parse(colorComponents[0]);
            green = float.Parse(colorComponents[1]);
            blue = float.Parse(colorComponents[2]);
            alpha = float.Parse(colorComponents[3]);

            playerRenderer.material.color = new Color(red, green, blue, alpha);
       }
       else 
       {
            kartRenderer.material.color = kartDefaultColor;
            playerRenderer.material.color = playerDefaultColor;
       }
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void OnButtonClick() 
    {
        if (carSwitcher.currentCarIndex == 0)
        {
            kartRenderer = kartClassicRenderer;
            playerRenderer = playerClassicRenderer;
        }
        else if (carSwitcher.currentCarIndex == 1)
        {
            kartRenderer = kartRoadsterRenderer;
            playerRenderer = playerRoadsterRenderer;
        }
        kartRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
        playerRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));

        PlayerPrefs.SetString("KartColor", kartRenderer.material.color.ToString());
        PlayerPrefs.SetString("PlayerColor", playerRenderer.material.color.ToString());
    }
}