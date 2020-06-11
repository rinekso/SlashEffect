using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;
    public float delayMove = .2f;
    public GameObject Player;
    public bool move=true;
    public bool isRight = false;
    public GameObject cover;
    public GameObject slashLine;
    public LineRenderer redLine;
    public Material Red;
    public Material Blue;
    Vector2 endPoint;
    public GameObject GameFinishPanel;
    public BarFinishScript barFinish;
    public GameObject GameOverPanel;
    public GameObject TutorialPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        slashLine.SetActive(false);
    }
    private void Update() {
        if(move){
            if(Input.GetMouseButtonDown(0)){
                DrawLine();
            }
            if(Input.GetMouseButton(0)){
                DragLine();
            }
            if(Input.GetMouseButtonUp(0)){  
                MouseRaycast();
                HideRedLine();
            }            
        }
    }
    void DrawLine(){
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        redLine.positionCount = 2;
        redLine.material = Blue;
        redLine.SetPosition(0,Player.GetComponent<PlayerAttribute>().PointSword.position);
        redLine.SetPosition(1,pos);
    }
    void DragLine(){
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        redLine.SetPosition(0,Player.GetComponent<PlayerAttribute>().PointSword.position);
        redLine.SetPosition(1,pos);

        float limitY = -4f;
        float yBase = -6.8f;
        float y = (pos.y<limitY)?yBase:pos.y;

        Vector3 destination = new Vector3(pos.x,y,Player.transform.position.z);
        RaycastMovement(Player.GetComponent<PlayerAttribute>().PointSword.position,destination);
    }
    public void HideRedLine(){
        redLine.positionCount = 1;
    }
    void MouseRaycast(){
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hit = Physics2D.RaycastAll(pos,Vector2.zero);
        bool p = false;
        for(int i = 0;i<hit.Length;i++){
            if(hit[i].transform.tag == "Player"){
                p = true;
            }
        }
        if(!p)
            MovePlayer(pos);
        else
            FocuseMode();
    }
    public float zoomFocus;
    bool focus = false;
    void FocuseMode(){
        focus = true;
        Player.GetComponent<Animator>().SetTrigger("Kuda");
        StartCoroutine(FocusCover(2));
        camera.GetComponent<CameraControll>().FocusMode(2);
    }
    void UnFocus(){
        focus = false;
        StopAllCoroutines();
        HideDrawSlash();
        camera.GetComponent<CameraControll>().UnFocus();
    }
    public void FocusCover(){
        StartCoroutine(FocusCover(1));
    }
    IEnumerator FocusCover(float time){
        cover.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        float focusAlpha = .78f;
        float t = 0;
        while(t < 1){
            yield return null;
            t += Time.deltaTime / time;
            cover.GetComponent<SpriteRenderer>().color = new Color(0,0,0,focusAlpha * t);
        }
        cover.GetComponent<SpriteRenderer>().color = new Color(0,0,0,focusAlpha);
    }
    IEnumerator StopMovement(){
        Rigidbody2D Rigid = Player.GetComponent<Rigidbody2D>();
        Rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        Player.GetComponent<PlayerAttribute>().gravityable = false;
        yield return new WaitForSeconds(delayMove);
        Player.GetComponent<PlayerAttribute>().gravityable = true;
        Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    IEnumerator Slash(){
        Player.GetComponent<PlayerAttribute>().animator.SetBool("Dash Status",false);
        Player.GetComponent<PlayerAttribute>().animator.SetBool("Slash Status",true);
        ShowDrawSlash();
        GetComponent<AudioController>().PlaySlash();

        // attack enemy
        AttackEnemy();

        yield return new WaitForSeconds(.1f);
        HideDrawSlash();

        yield return new WaitForSeconds(delayMove);
        Player.GetComponent<PlayerAttribute>().animator.SetBool("Slash Status",false);
    }
    void AttackEnemy(){
        float hit = Player.GetComponent<PlayerAttribute>().Hit;
        for (int i = 0; i < enemyTargets.Count; i++)
        {
            if(enemyTargets[i]){
                if(enemyTargets[i].GetComponent<EnemyAttribute>() != null)
                    enemyTargets[i].GetComponent<EnemyAttribute>().Hitted(hit);
                else if(enemyTargets[i].GetComponent<CoreScript>() != null)
                    enemyTargets[i].GetComponent<CoreScript>().Hitted(hit);
            }
        }
    }
    void DrawSlash(Vector3 start, Vector3 end){
        slashLine.GetComponent<LineRenderer>().SetPosition(0,start);
        slashLine.GetComponent<LineRenderer>().SetPosition(1,end);
    }
    void HideDrawSlash(){
        cover.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        slashLine.SetActive(false);
    }
    void ShowDrawSlash(){
        cover.GetComponent<SpriteRenderer>().color = new Color(0,0,0,200);
        slashLine.SetActive(true);
    }
    IEnumerator Dash(){
        Player.GetComponent<PlayerAttribute>().animator.SetBool("Slash Status",false);
        Player.GetComponent<PlayerAttribute>().animator.SetBool("Dash Status",true);
        GetComponent<AudioController>().PlayDash();
        yield return new WaitForSeconds(delayMove);
        Player.GetComponent<PlayerAttribute>().animator.SetBool("Dash Status",false);
    }
    public float speed;
    void MovePlayer(Vector2 pos){
        if(focus) UnFocus();
        StopCoroutine(StopMovement());
        StopCoroutine(Slash());
        StopCoroutine(Dash());
        // StopAllCoroutines();

        if(Player.GetComponent<PlayerAttribute>().EnoughStamina()){
            Player.GetComponent<PlayerAttribute>().Slash();
            bool tempIsRight = false;
            tempIsRight = (pos.x>Player.transform.position.x)?true:false;
            if(tempIsRight != isRight)
                FlipPlayer();

            DrawSlash(Player.GetComponent<PlayerAttribute>().PointSword.position,endPoint);
            Player.transform.position = endPoint;
            Player.GetComponent<PlayerAttribute>().CheckGroundManual();

            if(!Player.GetComponent<PlayerAttribute>().isGround)
                StartCoroutine(StopMovement());
            else{
                Rigidbody2D Rigid = Player.GetComponent<Rigidbody2D>();
                Player.GetComponent<PlayerAttribute>().gravityable = true;
                Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if(enemy)
                StartCoroutine(Slash());
            else
                StartCoroutine(Dash());
        }else{
            AndroidNativeFunctions.ShowToast("Not Enough Stamina");
        }

    }
    bool enemy;
    List<Transform> enemyTargets = new List<Transform>();
    void RaycastMovement(Vector2 start, Vector2 end){
        RaycastHit2D[] raycast;
        raycast = Physics2D.LinecastAll(start,end);

        if(enemyTargets.Count>0)
            enemyTargets.RemoveRange(0,enemyTargets.Count);

        enemy = false;
        endPoint = end;
        redLine.material = Blue;
        for(int i =0;i<raycast.Length;i++){
            if(raycast[i].transform.tag == "Enemy"){
                enemy = true;
                redLine.material = Red;
                enemyTargets.Add(raycast[i].transform);
            }

            if(raycast[i].transform.tag == "Ground"){
                Vector2 dir = (end-start).normalized;
                endPoint = raycast[i].point+dir*-1f;
            }
        }
    }
    void FlipPlayer(){
        if(isRight)
            Player.transform.localScale = new Vector3(1,1,1);
        else
            Player.transform.localScale = new Vector3(-1,1,1);
        
        isRight = !isRight;
    }
    public void Finish(bool status){
        StartCoroutine(FinishCount(status));
    }
    IEnumerator FinishCount(bool status){
        move = false;
        FocusCover();
        HideRedLine();
        yield return new WaitForSeconds(2);
        if(TutorialPanel){
            TutorialPanel.SetActive(false);
        }
        if(status){
            float currentExp = Player.GetComponent<PlayerAttribute>().exp;
            barFinish.addExp = currentExp;
            GameFinishPanel.SetActive(true);
        }else{
            GameOverPanel.SetActive(true);
        }
    }
}
