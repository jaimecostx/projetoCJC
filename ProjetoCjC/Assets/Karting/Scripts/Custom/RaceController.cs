using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class RaceController : MonoBehaviour
{
    public static RaceController instance;

    public TextMeshProUGUI [] positionsNames;
    public List<Kart> racers;
    public List<Kart> racersPositions;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SetPositionsText();
        InvokeRepeating("UpdatePositions", 0.5f, 0.5f);
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePositions()
    {
        racersPositions = racers.OrderByDescending(racer => racer.checkpointCounter).ThenBy(racer => racer.lastTime).ToList();
        SetPositionsText();
    }

    void SetPositionsText()
    {
        for (int i = 0; i < positionsNames.Length; i++)
        {
            positionsNames[i].text = racersPositions[i].racerName;
        }
    }
}
