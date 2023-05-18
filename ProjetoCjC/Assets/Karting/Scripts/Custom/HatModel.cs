using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{

    public class HatModel : MonoBehaviour
    {
        public GameObject[] hatPrefabs;
        public int currentHatIndex = 0;
        public GameObject classicHeadEND;
        public GameObject roadsterHeadEND;
        GameObject headEND;
        GameObject hatInstance;
        
        /// <summary>
        /// The Start method is called when the script instance is being initialized.
        /// It retrieves the value of the "CarModel" string from the PlayerPrefs and assigns the appropriate headEND GameObject.
        /// </summary>
        void Start()
        {
            if (PlayerPrefs.GetString("CarModel") == "0")
            {
                headEND = classicHeadEND;
            }
            else if (PlayerPrefs.GetString("CarModel") == "1")
            {
                headEND = roadsterHeadEND;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnButtonClick()
        {
            if (PlayerPrefs.GetString("CarModel") == "0")
            {
                headEND = classicHeadEND;
            }
            else if (PlayerPrefs.GetString("CarModel") == "1")
            {
                headEND = roadsterHeadEND;
            }

            hatCycle();
        }

        /// <summary>
        /// hatCycle method handles the logic for cycling through hatPrefabs and instantiating the selected hat.
        /// It also manages destroying the previous hat instance and updating PlayerPrefs with the current hat index.
        /// </summary>
        void hatCycle ()
        {
            if (hatInstance != null)
            {
                Destroy(hatInstance);
                hatInstance = null;
            }
            currentHatIndex = (currentHatIndex + 1) % (hatPrefabs.Length+1);

            if (currentHatIndex != hatPrefabs.Length){
                hatInstance = Instantiate(hatPrefabs[currentHatIndex]);
                hatInstance.transform.position = headEND.transform.position;
                hatInstance.transform.rotation = headEND.transform.rotation;
                hatInstance.transform.localScale = new Vector3(2f, 2f, 2f);
                hatInstance.transform.SetParent(headEND.transform);
            }
            PlayerPrefs.SetInt("HatModel", currentHatIndex);
        }
    }
}