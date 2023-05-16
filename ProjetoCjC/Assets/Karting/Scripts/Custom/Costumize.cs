using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumize : MonoBehaviour {

    public Renderer kartRenderer;
    public Renderer playerRenderer;

    Color kartDefaultColor;
    Color playerDefaultColor;

    // Start is called before the first frame update
    // Load already saved character costumization
    void Start() 
    {
       Debug.Log("start");
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

            Debug.Log(red + " " + green + " " + blue + " " + alpha);

            kartRenderer.material.color = new Color(red, green, blue, alpha);

            Debug.Log(PlayerPrefs.GetString("PlayerColor"));
            colorComponents = PlayerPrefs.GetString("PlayerColor").Replace("RGBA(", "").Replace(")", "").Split(',');

            // Parse the color components to floats
            red = float.Parse(colorComponents[0]);
            green = float.Parse(colorComponents[1]);
            blue = float.Parse(colorComponents[2]);
            alpha = float.Parse(colorComponents[3]);

            Debug.Log(red + " " + green + " " + blue + " " + alpha);

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
        Debug.Log("Customize Button clicked!");
        kartRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));
        playerRenderer.material.color = new Color(Random.Range(0f, 1f),Random.Range(0f, 1f),Random.Range(0f, 1f));

        PlayerPrefs.SetString("KartColor", kartRenderer.material.color.ToString());
        PlayerPrefs.SetString("PlayerColor", playerRenderer.material.color.ToString());
    }
}