using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float damage;
    private Health health;
    private Animator animator;
    private Rigidbody2D rb2D;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private bool isFacingRight;
    private bool isAttacking;
    private float moveHorizontal;
    private float moveVertical;

    private float startingGravity;

    //ledge grab variables
    private bool upperBox, lowerBox;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float upperXOfSet, upperYOfSet, upperXSize, upperYSize, lowerXOfSet, lowerYOfSet, lowerXSize, lowerYSize;
    private bool isGrabingLedge;
    private bool dissableHorizontalMovement;


    private EnemyHealth enemyHealth;
    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;
    [SerializeField] LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        moveSpeed = 0.75f;
        jumpForce = 15f;
        isJumping = false;
        isFacingRight = false;
        isAttacking = false;
        isGrabingLedge = false;
        dissableHorizontalMovement = false;

        startingGravity = rb2D.gravityScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("speed", Mathf.Abs(moveHorizontal));

        if (Input.GetKeyDown("space") && !dissableHorizontalMovement)
        {
            Attack();
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Dash-Attack")){
            isAttacking = true;
        }
        else{
            isAttacking = false;
        }

        //Ledge grab

        upperBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (upperXOfSet * transform.localScale.x), transform.position.y + upperYOfSet), new Vector2(upperXSize,upperYSize), 0f, groundMask);
        lowerBox = Physics2D.OverlapBox(new Vector2(transform.position.x + (lowerXOfSet * transform.localScale.x), transform.position.y + lowerYOfSet), new Vector2(lowerXSize,lowerYSize), 0f, groundMask);

        if(lowerBox && !upperBox && !isGrabingLedge && isJumping){
            isGrabingLedge = true;
        }
    }

    void FixedUpdate(){

        if((moveHorizontal > 0.1f || moveHorizontal < -0.1f) && !dissableHorizontalMovement){
            if(isAttacking){
                rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed/3, 0f), ForceMode2D.Impulse);
            }
            else{
                rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            }

        }

        if(moveVertical > 0.1f && !isJumping){
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
        
        if(moveVertical > 0.1f && isGrabingLedge){
            isGrabingLedge = false;
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce/2), ForceMode2D.Impulse);
            rb2D.gravityScale = startingGravity;
            dissableHorizontalMovement = false;
            animator.SetTrigger("jump");
        }

        if(moveHorizontal > 0 && isFacingRight && !dissableHorizontalMovement){
            FlipPlayer();
        }
        if(moveHorizontal < 0 && !isFacingRight && !dissableHorizontalMovement){
            FlipPlayer();
        }

        if(isGrabingLedge){
            animator.SetTrigger("grabLedge");
            rb2D.velocity = new Vector2(0f, 0f);
            rb2D.gravityScale = 0f;
            dissableHorizontalMovement = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Platform"){
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        //Checking if player jumped on spikes
        if(collision.gameObject.layer == 9){
            health.TakeDamage(3);
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

    }
    
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Platform"){
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

    }

    void FlipPlayer(){
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    void Attack(){
        animator.SetTrigger("attack");
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (upperXOfSet * transform.localScale.x), transform.position.y + upperYOfSet), new Vector2(upperXSize,upperYSize));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(transform.position.x + (lowerXOfSet * transform.localScale.x), transform.position.y + lowerYOfSet), new Vector2(lowerXSize,lowerYSize));
    }


    private bool EnemyInSight(){

        RaycastHit2D hit = Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y,capsuleCollider.bounds.size.z), 0, Vector2.left, 0, enemyLayer);


        if(hit.collider != null){
            enemyHealth = hit.transform.GetComponent<EnemyHealth>();
        }

        return hit.collider != null;
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(capsuleCollider.bounds.size.x * range,capsuleCollider.bounds.size.y,capsuleCollider.bounds.size.z));
    }

        private void DamageEnemy(){
        if(EnemyInSight()){
            enemyHealth.TakeDamage(damage);
        }
    }
}
