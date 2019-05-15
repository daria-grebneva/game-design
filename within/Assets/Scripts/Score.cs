using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Dictionary<string, string> results = GameResult.GetParameters();
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        results = GameResult.GetParameters();
        GameObject.Find("Wins").GetComponent<Text>().text = results["WINS"].ToString();
        GameObject.Find("Loses").GetComponent<Text>().text = results["LOSES"].ToString();
    }
}
