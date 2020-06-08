using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    bool exit = false;
    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && exit==true){
			Application.Quit();
		}else if (Input.GetKeyDown(KeyCode.Escape) && exit==false){
			exit = true;
			AndroidNativeFunctions.ShowToast("Press back button again to exit");
			StartCoroutine(cancelExit());
		}
    }
	IEnumerator cancelExit(){
		yield return new WaitForSeconds(2);
		exit = false;
	}
}
