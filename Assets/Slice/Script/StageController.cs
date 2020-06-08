using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public int currentLevel;
    public int currentStage;
    public GameObject btnNextLevel;
    private void Start() {
        if(!Application.CanStreamedLevelBeLoaded(currentStage+"-"+(currentLevel+1))){
            btnNextLevel.GetComponent<Button>().interactable = false;
        }
    }
    public void NextLevel(){
        SceneManager.LoadScene(currentStage+"-"+(currentLevel+1));
    }
    public void Retry(){
        SceneManager.LoadScene(currentStage+"-"+currentLevel);
    }
    public void Mainmenu(){
        SceneManager.LoadScene("Mainmenu");
    }

}
