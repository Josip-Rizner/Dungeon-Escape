using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float attackCooldown;
    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;

    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;

    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private EnemyHealth health;
    void Start()
    {
        health = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        
    }   


    void Update()
    {

        cooldownTimer += Time.deltaTime;

        if(PlayerInSight() && !playerHealth.isDead){
            if(cooldownTimer >= attackCooldown){
                
                cooldownTimer = 0;
                animator.SetTrigger("attack");
            }
        }


        if(enemyPatrol != null){
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    void FixedUpdate(){


    }

    private bool PlayerInSight(){

        RaycastHit2D hit = Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y,capsuleCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);


        if(hit.collider != null){
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(capsuleCollider.bounds.size.x * range,capsuleCollider.bounds.size.y,capsuleCollider.bounds.size.z));
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            playerHealth.TakeDamage(damage);
        }
    }
}
