using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game{

    public class HatModel : MonoBehaviour
    {
        public GameObject[] hatPrefabs;
        public int currentHatIndex = 0;
        public GameObject headEnd;
        GameObject hatInstance;
        GameObject carObject;
        CarModel car;
        // Start is called before the first frame update
        void Start()
        {
            carObject = GameObject.Find("KartClassic_Player");
        }

        // Update is called once per frame
        void Update()
        {
        
        }

    public void OnButtonClick()
    {
        if (PlayerPrefs.GetString("CarModel") == "1")
        {
            carObject = GameObject.Find("Roadster_Player");
            headEnd = carObject.transform.Find("KartVisual/PlayerIdle/Root1/Hips/Spine1/Spine2/Neck/Head/HeadEND");
        }
        else
        {
            carObject = GameObject.Find("KartClassic_Player");
            headEnd = carObject.transform.Find("KartBody/PlayerIdle/Root1/Hips/Spine1/Spine2/Neck/Head/HeadEND");
        }
        
        hatCycle();
    }

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
            hatInstance.transform.position = headEnd.transform.position;
            hatInstance.transform.rotation = headEnd.transform.rotation;
            hatInstance.transform.localScale = new Vector3(2f, 2f, 2f);
            hatInstance.transform.SetParent(headEnd.transform);
        }
        PlayerPrefs.SetInt("HatModel", currentHatIndex);
    }
}

}