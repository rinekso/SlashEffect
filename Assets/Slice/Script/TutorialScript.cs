using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public string[] tutorialText;
    public TMPro.TextMeshProUGUI textTutor;
    public PlayerAttribute player;
    public PlayerMovement playerMovement;
    int currentTutor = -1;
    bool first = false;
    public void Next(){
        currentTutor++;
        if(currentTutor < tutorialText.Length){
            textTutor.text = tutorialText[currentTutor];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTutor = -1;
        Next();
        StartCoroutine(WaitForNext(4));
    }
    IEnumerator WaitForNext(float sec){
        for (int i = 0; i < tutorialText.Length; i++)
        {
            yield return new WaitForSeconds(sec);
            Next();            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
