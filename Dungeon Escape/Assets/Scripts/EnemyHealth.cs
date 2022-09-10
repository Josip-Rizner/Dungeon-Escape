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
    private EnemyPatrol enemyPatrol;

    [SerializeField] AudioClip gettingHurtSound;
    [SerializeField] AudioClip dyingSound;

    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        isDead = false;

        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    public void TakeDamage(float _damage){

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        //Debug.Log(currentHealth);

        if(currentHealth > 0){
            
            animator.SetTrigger("damaged");
            SoundController.instance.PlaySound(gettingHurtSound);
            enemyPatrol.TurnWhenAttacked();
        }
        else{
            if(!isDead){
                animator.SetTrigger("dead");
                SoundController.instance.PlaySound(dyingSound);
                GetComponentInParent<EnemyPatrol>().enabled = false;
                GetComponent<EnemyController>().enabled = false;
                isDead = true;

                capsuleCollider.size = new Vector2(0.2f, 0.1f);
                capsuleCollider.offset = new Vector2(0f, -0.2f);
            }
        }
    }

    public bool checkIfDead(){
        return isDead;
    }


    void Update(){
        /*if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }*/
    }


}
