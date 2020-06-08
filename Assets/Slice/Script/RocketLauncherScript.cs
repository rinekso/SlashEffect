using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherScript : MonoBehaviour
{
    public float cooldown;
    public bool isRight = true;
    Transform target;
    public GameObject Projectille;
    public Transform[] hands;
    public bool PlayerIn;
    public bool shooting = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            PlayerIn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            PlayerIn = false;
        }
    }
    void Shoot(){
        for(int  i = 0;i<hands.Length;i++){
            if(hands[i] != null){
                Instantiate(Projectille,hands[i].position, Quaternion.Euler(0,0,90));
            }
        }
    }
    IEnumerator CoolDownShoot(){
        shooting = true;
        Shoot();
        yield return new WaitForSeconds(cooldown);
        shooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerIn && !shooting){
            StartCoroutine(CoolDownShoot());
        }
    }

}
