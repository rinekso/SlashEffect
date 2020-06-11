using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour
{
    public float maxHP;
    public float currentHp;
    public GameObject ParticleDie;
    public bool CallUIHPBar = false;
    public GameObject UIHpIndicator;
    public GameObject HPPoin;
    public int exp;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHP;
        if(CallUIHPBar){
            Transform Canvas = GameObject.Find("Canvas").transform;
            GameObject indicator = Instantiate(UIHpIndicator,Canvas);
            indicator.GetComponent<IndicatorUI>().obj = HPPoin;
            indicator.GetComponent<HPIndicator>().maxHp = maxHP;
            indicator.GetComponent<HPIndicator>().Hitted(maxHP);
            UIHpIndicator = indicator;
        }else{
            if(UIHpIndicator != null)
                UIHpIndicator.GetComponent<HPIndicator>().maxHp = maxHP;
        }
    }
    public void Hitted(float amount){
        currentHp -= amount;
        if(UIHpIndicator != null)
            UIHpIndicator.GetComponent<HPIndicator>().Hitted(currentHp);
        if(currentHp <= 0){
            GameOver();
        }
    }
    void GameOver(){
        Instantiate(ParticleDie,transform.position,new Quaternion());
        if(CallUIHPBar){
            Destroy(UIHpIndicator);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttribute>().BonusExp(exp);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMovement>().Finish(true);
        Destroy(gameObject);
    }
}
