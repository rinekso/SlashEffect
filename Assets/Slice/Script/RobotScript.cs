using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    public float cooldown;
    public bool isRight = true;
    Animator animator;
    Transform target;
    public GameObject Projectille;
    public Transform[] hands;
    public bool PlayerIn;
    public bool shooting = false;
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            target = other.transform;
            if((other.transform.position.x+2f) < transform.position.x){
                if(isRight)
                    Flip(); 
            }else if((other.transform.position.x-2f) > transform.position.x){
                if(!isRight)
                    Flip();
            }
        }
    }
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
    void Flip(){
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        isRight = !isRight;
    }
    void Shoot(){
        for(int  i = 0;i<hands.Length;i++){
            if(hands[i] != null){
                if(isRight)
                    Instantiate(Projectille,hands[i].position,new Quaternion());
                else
                    Instantiate(Projectille,hands[i].position, Quaternion.Euler(0,0,180));
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    IEnumerator CoolDownShoot(){
        shooting = true;
        animator.SetTrigger("shoot");
        yield return new WaitForSeconds(.5f);
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
