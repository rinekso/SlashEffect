using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaIndicator : MonoBehaviour
{
    public GameObject value;
    float maxWidth;
    public float maxHp;
    float currentHp;
    public float staminaCost;
    public Color normal;
    public Color crisis;
    // Start is called before the first frame update
    void Start()
    {
        maxWidth = GetComponent<RectTransform>().rect.size.x;
    }
    public void Hitted(float hp){
        currentHp = hp;
        float widthValue = hp/maxHp*maxWidth;
        // print(widthValue+"/"+hp);
        value.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthValue,value.GetComponent<Image>().rectTransform.sizeDelta.y);
        if(staminaCost >= currentHp){
            value.GetComponent<Image>().color = crisis;
        }else{
            value.GetComponent<Image>().color = normal;
        }
    }
}
