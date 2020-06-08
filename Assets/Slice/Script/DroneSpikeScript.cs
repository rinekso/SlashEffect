using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpikeScript : MonoBehaviour
{
    public GameObject dieParticle;
    public float HitAmount;
    public GameObject area;
    Transform target;
    bool move = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AddTarget(GameObject targetConfirm){
        target = targetConfirm.transform;
        GetComponent<Animator>().SetTrigger("walk");
    }
    public void RemoveTarget(){
        target = null;
        GetComponent<Animator>().SetTrigger("idle");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            PlayerAttribute PA = other.transform.parent.GetComponent<PlayerAttribute>();
            PA.Hitted(HitAmount,transform);
            StartCoroutine(Moveable());
            if(PA.die)
                RemoveTarget();
            // DestroySelf();
        }
    }
    void DestroySelf(){
        Instantiate(dieParticle,transform.position, new Quaternion());
        Destroy(gameObject);
    }
    IEnumerator Moveable(){
        move = false;
        yield return new WaitForSeconds(1);
        move = true;
    }

    public float speed;
    // Update is called once per frame
    void Update()
    {
        if(target!=null && move){
            Vector3 dir = (target.position-transform.position).normalized;
            transform.position += dir*speed;

            if((target.position.x+2f) < transform.position.x){
                if(isRight)
                    Flip(); 
            }else if((target.position.x-2f) > transform.position.x){
                if(!isRight)
                    Flip();
            }

        }
    }
    bool isRight = true;
    void Flip(){
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        isRight = !isRight;
    }}
