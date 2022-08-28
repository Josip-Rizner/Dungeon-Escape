using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float startingHealth;

    public float currentHealth{ get; private set; }

    private Animator animator;
    private bool isDead;

    private CapsuleCollider2D capsuleCollider;


    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        isDead = false;
    }

    public void TakeDamage(float _damage){

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        //Debug.Log(currentHealth);

        if(currentHealth > 0){
            
            animator.SetTrigger("damaged");
        }
        else{
            if(!isDead){
                animator.SetTrigger("dead");
                GetComponentInParent<EnemyPatrol>().enabled = false;
                GetComponent<SkeletonController>().enabled = false;
                isDead = true;

                capsuleCollider.size = new Vector2(0.2f, 0.1f);
                capsuleCollider.offset = new Vector2(0f, -0.2f);
            }
        }
    }



    void Update(){
        /*if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }*/
    }


}
