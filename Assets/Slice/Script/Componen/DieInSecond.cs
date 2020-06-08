using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInSecond : MonoBehaviour
{
    public float seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die(){
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

}
