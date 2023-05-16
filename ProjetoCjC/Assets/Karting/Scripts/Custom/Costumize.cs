using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumize : MonoBehaviour {

    public Renderer kartRenderer;
    public Renderer playerRenderer;

    // Start is called before the first frame update
    void Start() 
    {
       Debug.Log("start");
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