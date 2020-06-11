using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFinishScript : MonoBehaviour
{
    public float lastExp;
    public float addExp;
    public GameObject value;
    public TMPro.TextMeshProUGUI levelText;
    float maxWidth;
    float lvlExt = 1000;
    float maxLvlExt;
    // Start is called before the first frame update
    void Start()
    {
        lastExp = PlayerPrefs.GetFloat("exp");
        print(lastExp);

        int currentLevel = PlayerPrefs.GetInt("lvl");
        if(currentLevel == 0){
            currentLevel = 1;
            PlayerPrefs.SetInt("lvl",1);
        }

        levelText.text = "Level "+currentLevel;
        for (int i = 0; i < currentLevel; i++)
        {
            if(i == 0){
                maxLvlExt = lvlExt;
            }else{
                maxLvlExt = float.Parse((maxLvlExt*1.2)+"");
            }
        }

        maxWidth = GetComponent<RectTransform>().sizeDelta.x;
        StartCoroutine(FillExp());
    }
    IEnumerator FillExp(){
        float duration = 2;
        float t = 0;
        while (t<1)
        {
            yield return null;
            t += Time.deltaTime/duration;
            float widthValue = (lastExp+(addExp*t))/maxLvlExt*maxWidth;
            if(widthValue>=maxWidth){
                LevelUp(t);
                break;
            }else{
                value.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthValue,value.GetComponent<Image>().rectTransform.sizeDelta.y);
            }
        }
        value.GetComponent<Image>().rectTransform.sizeDelta = new Vector2((lastExp+(addExp))/maxLvlExt*maxWidth,value.GetComponent<Image>().rectTransform.sizeDelta.y);
        PlayerPrefs.SetFloat("exp",lastExp+addExp);
    }
    void LevelUp(float t){
        value.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(0,value.GetComponent<Image>().rectTransform.sizeDelta.y);
        int currentLevel = PlayerPrefs.GetInt("lvl");
        int poin = PlayerPrefs.GetInt("poin");
        PlayerPrefs.SetInt("lvl",currentLevel+1);
        PlayerPrefs.SetInt("poin",poin+1);
        levelText.text = "Level "+(currentLevel+1);
        lastExp=0;
        addExp = addExp-(addExp*t);
        StopCoroutine(FillExp());
        StartCoroutine(FillExp());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
