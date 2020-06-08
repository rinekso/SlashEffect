using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTab : MonoBehaviour
{
    [System.Serializable]
    public struct Panel{
        public GameObject button;
        public GameObject panel;
    }
    public Panel[] panels;
    public void OpenPanel(int id){
        for (int i = 0; i < panels.Length; i++)
        {
            if(i != id){
                ButtonDown(i);
                panels[i].panel.SetActive(false);
            }else{
                ButtonUp(i);
                panels[i].panel.SetActive(true);
            }
        }
    }
    void ButtonUp(int id){
        panels[id].button.GetComponent<Image>().color = new Color(255,255,255,255);
        panels[id].button.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0,0,0,255);
    }
    void ButtonDown(int id){
        panels[id].button.GetComponent<Image>().color = new Color(0,0,0,255);
        panels[id].button.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(255,255,255,255);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
