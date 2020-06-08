    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInSecond : MonoBehaviour
{
    public float seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disable());
    }
    IEnumerator disable(){
        yield return new WaitForSeconds(seconds);
        print("disable");
        gameObject.SetActive(false);
    }

}
