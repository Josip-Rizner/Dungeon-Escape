using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{

    [SerializeField] float attackCooldown;
    [SerializeField] int damage;
    [SerializeField] float range;
    [SerializeField] float colliderDistance;

    [SerializeField] CapsuleCollider2D capsuleCollider;
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        cooldownTimer += Time.deltaTime;


        if(PlayerInSight()){
            if(cooldownTimer >= attackCooldown){
                
                cooldownTimer = 0;
                animator.SetTrigger("attack");
            }
        }
    }

    void FixedUpdate(){


    }

    private bool PlayerInSight(){

        RaycastHit2D hit = Physics2D.BoxCast(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(capsuleCollider.bounds.size.x * range, capsuleCollider.bounds.size.y,capsuleCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }


    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(capsuleCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(capsuleCollider.bounds.size.x * range,capsuleCollider.bounds.size.y,capsuleCollider.bounds.size.z));
    }

    private void DamagePlayer(){
        if(PlayerInSight()){
            //Damage player
        }
    }
}
