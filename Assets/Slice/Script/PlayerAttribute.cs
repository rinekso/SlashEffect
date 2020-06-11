using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public bool isGround;
    public Transform GroundCheck;
    public Transform PointSword;
    public LayerMask WhatIsGround;
    const float GroundRadius = .01f;
    Rigidbody2D rigidbody;
    public Animator animator;
    public float gravity;
    public bool gravityable;
    public float Hit;
    public float maxHp;
    public float maxStamina;
    float currentHp;
    float currentStamina;
    public float staminaCost;
    public float regenStamina;
    public GameObject UIHpIndicator; 
    public StaminaIndicator UIStaminaIndicator;
    public bool die = false;
    public int exp;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitPlayer());
    }
    IEnumerator InitPlayer(){
        ReadLevel();
        yield return null;
        UIHpIndicator.GetComponent<HPIndicator>().maxHp = maxHp;
        UIStaminaIndicator.GetComponent<StaminaIndicator>().maxHp = maxStamina;
        UIStaminaIndicator.GetComponent<StaminaIndicator>().staminaCost = staminaCost;
        currentHp = maxHp;
        currentStamina = maxStamina;
        gravityable = true;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        yield return null;
    }
    void ReadLevel(){
        if(PlayerPrefs.GetFloat("maxHpamount") > 0)
            maxHp = PlayerPrefs.GetFloat("maxHpamount");
        if(PlayerPrefs.GetFloat("maxSpamount") > 0)
            maxStamina = PlayerPrefs.GetFloat("maxSpamount");
        if(PlayerPrefs.GetFloat("regenSpamount") > 0)
            regenStamina = PlayerPrefs.GetFloat("regenSpamount")/6;
        if(PlayerPrefs.GetFloat("APamount") > 0)
            Hit = PlayerPrefs.GetFloat("APamount");
    }
    public bool EnoughStamina(){
        return (staminaCost<=currentStamina)? true: false;
    }
    public void Slash(){
        currentStamina -= staminaCost;
    }
    public void Hitted(float amount,Transform Enemy){
        currentHp -= amount;
        UIHpIndicator.GetComponent<HPIndicator>().Hitted(currentHp);
        if(currentHp <= 0){
            GameOver();
        }else{
            StartCoroutine(KnockBackMove(Enemy.position));
        }
    }
    public float forceKnockBack;
    IEnumerator KnockBackMove(Vector3 EnemyPosition){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 currentPosition = transform.position;
        Vector3 direction = (currentPosition-EnemyPosition).normalized;
        transform.position += new Vector3(direction.x,0,0)*forceKnockBack;
        yield return null;
    }
    void GameOver(){
        print("gameover");
        die = true;
        animator.SetBool("die",true);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerMovement>().Finish(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGround = false;
        animator.SetBool("InAir",true);
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundRadius, WhatIsGround);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GroundCheck.position,new Vector2(1,.1f),WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Ground" || colliders[i].tag == "GroundAir" || colliders[i].tag == "Enemy"){
                isGround = true;
                animator.SetBool("InAir",false);
            }
        }

        if(gravityable && !isGround)
            rigidbody.velocity = new Vector3(0, -gravity, 0);

        if(currentStamina+regenStamina>maxStamina)
            currentStamina = maxStamina;
        else
            currentStamina += regenStamina;
        UIStaminaIndicator.GetComponent<StaminaIndicator>().Hitted(currentStamina);
    }
    public void BonusStamina(float amount){
        if(currentStamina + amount < maxStamina)
            currentStamina += amount;
        else
            currentStamina = maxStamina;
    }
    public void BonusExp(int amount){
        exp += amount;
    }
    public void CheckGroundManual(){
        isGround = false;
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundRadius, WhatIsGround);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(GroundCheck.position,new Vector2(1,.1f),WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Ground" || colliders[i].tag == "Enemy" || colliders[i].tag == "GroundAir"){
                isGround = true;
            }
        }
    }
}
