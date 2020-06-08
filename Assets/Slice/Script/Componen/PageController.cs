using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageController : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject BackButton;
    int targetPage = 0;
    private void Start() {
        BackLogic();
    }
    public void openPage(int target){
        targetPage = target;
        for(int i = 0;i<pages.Length;i++){
            if(i != target)
                pages[i].SetActive(false);
            else
                pages[i].SetActive(true);
        }
        BackLogic();
    }
    void BackLogic(){
        if(targetPage != 0){
            BackButton.SetActive(true);
        }else{
            BackButton.SetActive(false);
        }
    }
}
