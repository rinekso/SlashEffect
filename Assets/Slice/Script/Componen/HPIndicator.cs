using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPIndicator : MonoBehaviour
{
    public GameObject value;
    public float maxWidth = 0;
    public float maxHp;
    float currentHp;
    // Start is called before the first frame update
    void Start()
    {
        if(maxWidth == 0)
            maxWidth = GetComponent<RectTransform>().rect.size.x;
    }
    public void Hitted(float hp){
        currentHp = hp;
        float widthValue = hp/maxHp*maxWidth;
        // print(maxHp+"/"+hp+"/"+maxWidth);
        value.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthValue,value.GetComponent<Image>().rectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
