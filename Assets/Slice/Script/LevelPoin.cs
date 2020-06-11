using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoin : MonoBehaviour
{
    public int poin;
    private void Start() {
        PlayerPrefs.SetInt("poin",1);
        poin = PlayerPrefs.GetInt("poin");
        GetComponent<TMPro.TextMeshProUGUI>().text = "Level poin : "+poin;
    }
    public void DecreesePoin(){
        poin = PlayerPrefs.GetInt("poin");
        print(poin);
        if(poin > 0){
            PlayerPrefs.SetInt("poin",poin-1);
            print(poin-1);
            GetComponent<TMPro.TextMeshProUGUI>().text = "Level poin : "+(poin-1);        
        }
    }
}
