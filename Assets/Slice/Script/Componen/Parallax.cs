using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float lenght, startPosX;
    public float startPosY;
    public GameObject Cam;
    public float parallaxEffectX;
    public float parallaxEffectY;
    // Start is called before the first frame update
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    public void setHeight(){
        float h = Cam.GetComponent<Camera>().orthographicSize*2;
        lenght = h*Cam.GetComponent<Camera>().aspect;
    }
    // Update is called once per frame
    void Update()
    {
        // setHeight();
        float temp = (Cam.transform.position.x * (1-parallaxEffectX));
        float distX = (Cam.transform.position.x * parallaxEffectX);
        float distY = (Cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        if(temp > startPosX + lenght) startPosX += lenght;
        else if(temp < startPosX - lenght) startPosX -= lenght;
    }
}
