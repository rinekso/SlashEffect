using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public float speed;
    Vector3 dir;
    public GameObject dieParticle;
    Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    float angle;
    float targetRot;
    public float rotAb;
    public float HitAmount;
    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.tag == "Player"){
            other.transform.parent.GetComponent<PlayerAttribute>().Hitted(HitAmount,transform);
            DestroySelf();
        }else if(other.transform.tag == "Ground"){
            DestroySelf();
        }
    }
    void DestroySelf(){
        Instantiate(dieParticle,transform.position, new Quaternion());
        Destroy(gameObject);
    }
    float distTemp = 0;
    bool rotRight = true;
    // Update is called once per frame
    void Update()
    {
        dir = (transform.GetChild(0).transform.position-transform.position).normalized;
        transform.position += dir*speed;

        if(target != null){
            targetPos = target.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            targetRot = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;

            float angle = transform.eulerAngles.z;
            targetRot = (targetRot < 0) ? 180+(180+targetRot) : targetRot;
            float trueAngle = (angle > 180) ? angle - 360 : angle;
            float dist = Mathf.Abs(targetRot-angle);
            dist = (dist>180)?360-dist:dist;
            float rotFinal=0;
            if(distTemp<dist){
                flipRot();
            }
            distTemp = dist;
            rotFinal = (rotRight) ? rotAb:-rotAb;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, trueAngle+rotFinal));
        }
    }
    void flipRot(){
        rotRight = !rotRight;
    }
}
