using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ground;

    void Start()
    {
        Debug.Log("Comecei");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider with: " + collision);
    }

    void OnTriggerEnter(Collider other)
    {
        // Do something with the other collider
        Debug.Log("Triggered with: " + other.gameObject.name);
    }


}
