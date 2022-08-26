using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startingHealth;

    public float currentHealth{ get; private set; }

    private Animator animator;
    private bool dead;
    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        dead = false;
    }

    public void TakeDamage(float _damage){

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        //Debug.Log(currentHealth);

        if(currentHealth > 0){
            
            animator.SetTrigger("damaged");
        }
        else{
            if(!dead){
                animator.SetTrigger("dead");
                GetComponent<PlayerController>().enabled = false;
                dead = true;
            }
        }
    }


    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }
    }
}
