using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashController : MonoBehaviour
{
    public GameObject crashObject;
    GameObject obj;


    List<Vector3> positionsList = new List<Vector3>(){
        new Vector3(20.86943f, 0.1999993f, -88.80925f),
        new Vector3(112.9f, 25.668f, 60.591f),
        new Vector3(-43f, 0.35f, 62.58f),
        new Vector3(-45.28f, 1.41f, -1.78f),
        new Vector3(-4.81f, 1.16f, 1.2f),
        new Vector3(-102.27f, 1.16f, -48.04f),
        new Vector3(-56.53f, 0.67f, -51.74f),
        new Vector3(2.3f, 0.67f, -73.98f),
        new Vector3(69.69f, 0.67f, -73.98f),
        new Vector3(85.96f, 0.98f, 36.2f),
    };

    // Start is called before the first frame update
    void Start()
    {   
        for(int i=0; i<3;i++){
            int index = Random.Range(0,positionsList.Count-1);
            obj = Instantiate(crashObject, positionsList[index], Quaternion.identity);
            //obj.Tag = "pin";
            CrashObject crashObj = obj.GetComponent<CrashObject>();
            crashObj.TimeGained = 10.0f;
            positionsList.RemoveAt(index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnObject(){
            Debug.Log(positionsList.Count);
            if(positionsList.Count>0){
                int index = Random.Range(0,positionsList.Count-1);
                obj = Instantiate(crashObject, positionsList[index], Quaternion.identity);
                //obj.Tag = "pin";
                CrashObject crashObj = obj.GetComponent<CrashObject>();
                crashObj.TimeGained = 10.0f;
                positionsList.RemoveAt(index);
            }
    }

}

class Position
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

   public Position(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
