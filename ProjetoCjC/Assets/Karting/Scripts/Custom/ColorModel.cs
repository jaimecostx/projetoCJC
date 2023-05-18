using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

namespace Game{

    public class ColorModel : MonoBehaviour
    {
        public Renderer kartClassicRenderer;
        public Renderer kartRoadsterRenderer;

        public Renderer playerClassicRenderer;
        public Renderer playerRoadsterRenderer;

        public GameObject classicHeadEND;
        public GameObject roadsterHeadEND;

        Renderer modelRenderer;

        public string model;
        Dictionary<string, Color> colors = new ();

        CarModel car;
        
        void Start() 
        {
            colors.Add("RedButton", Color.red);
            colors.Add("BlueButton", Color.blue);
            colors.Add("GreenButton", new Color(0f, 0.5f, 0f)); // mudar fica invisivel
            colors.Add("PinkButton", new Color(1f, 0.5f, 0.5f));
            colors.Add("OrangeButton", new Color(1f, 0.5f, 0f));
            colors.Add("YellowButton", Color.yellow);
            colors.Add("BrownButton", new Color(0.6f, 0.4f, 0.2f));
            colors.Add("WhiteButton", new Color(1f, 1f, 1f));
            colors.Add("PurpleButton", new Color(0.5f, 0f, 0.5f));
            colors.Add("BlackButton", Color.black);
        }

    
        public void OnButtonClick()
        {
            if (model == "Car")
            {
                Debug.Log("ColorModel - OnButtonClick() - Model Car");
                if (PlayerPrefs.GetInt("CarModel") == 0)
                {
                    modelRenderer = kartRoadsterRenderer;
                }
                else if (PlayerPrefs.GetInt("CarModel") == 1)
                {
                    modelRenderer = kartClassicRenderer;
                }
                Color temp;
                colors.TryGetValue(gameObject.name, out temp);
                modelRenderer.material.color = temp;
            }
            if (model == "Player")
            {
                Debug.Log("ColorModel - OnButtonClick() - Player Car");
                if (PlayerPrefs.GetInt("CarModel") == 0)
                {
                    modelRenderer = playerRoadsterRenderer;
                }
                else if (PlayerPrefs.GetInt("CarModel") == 1)
                {
                    modelRenderer = playerClassicRenderer;
                }
                Color temp;
                colors.TryGetValue(gameObject.name, out temp);
                modelRenderer.material.color = temp;
            }


            
        }
    }
}