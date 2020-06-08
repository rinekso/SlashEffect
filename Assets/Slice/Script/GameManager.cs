using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        int first = PlayerPrefs.GetInt("firsttime");
        if(first == 0){
            FirstPlay();
        }
    }
    void FirstPlay(){
        PlayerPrefs.SetInt("LevelPlayer",1);
        PlayerPrefs.SetInt("HP",1);
        PlayerPrefs.SetInt("SP",1);
        PlayerPrefs.SetInt("RegenSP",1);
        PlayerPrefs.SetInt("AP",1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
