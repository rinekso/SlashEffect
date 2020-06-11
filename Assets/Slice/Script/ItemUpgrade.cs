using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
    public string key;
    public string satuan;
    public float value;
    public float upgradeValue;
    public TMPro.TextMeshProUGUI buttonBuy;
    public TMPro.TextMeshProUGUI currentLevel;
    public LevelPoin levelPoin;
    // Start is called before the first frame update
    void Start()
    {
        ShowLevel();
    }
    void ShowLevel(){
        buttonBuy.text = "Upgrade\n(+"+upgradeValue+" "+satuan+")";
        int level = PlayerPrefs.GetInt(key);
        if(level == 0){
            level = 1;
            PlayerPrefs.SetInt(key,1);
        }            

        currentLevel.text = "Lvl "+level+" ("+(value+(upgradeValue*(level-1)))+" "+satuan+")";
        PlayerPrefs.SetFloat(key+"amount",value+(upgradeValue*(level-1)));
    }
    public void Upgrade(){
        int poin = PlayerPrefs.GetInt("poin");
        if(poin > 0){
            int level = PlayerPrefs.GetInt(key);
            PlayerPrefs.SetInt(key,level+1);
            levelPoin.DecreesePoin();
            ShowLevel();
        }else{
            AndroidNativeFunctions.ShowToast("Not enough level poin");
        }
    }
}
