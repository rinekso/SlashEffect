using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorUI : MonoBehaviour {
    public GameObject obj;
    Camera camera;
    private RectTransform rt;
 
    // Use this for initialization
    void Start () {
        camera = Camera.main;
        rt = GetComponent<RectTransform>();
    }
    private void OnGUI() {
        if(obj){
            Transform gotTrans = obj.GetComponent<Transform>();
            rt.position = camera.WorldToScreenPoint(gotTrans.position);
        }else{
            Destroy(gameObject);
        }
    }

}
