using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Costumize : MonoBehaviour {

    public Renderer kartRenderer;
    public Renderer playerRenderer;

    // Start is called before the first frame update
    void Start() {
       Debug.Log("start");
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnButtonClick() 
    {
        Debug.Log("Button clicked!");
    }
}