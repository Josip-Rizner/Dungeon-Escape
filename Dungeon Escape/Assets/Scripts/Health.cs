using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startingHealth;

    public float currentHealth{ get; private set; }

    private Animator animator;
    public bool isDead{ get; private set; }



    //iFrames
    [SerializeField] float invulnerabilityDuration;
    [SerializeField] int numberOfFlashes; 
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        isDead = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage){

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        //Debug.Log(currentHealth);

        if(currentHealth > 0){
            
            animator.SetTrigger("damaged");
            StartCoroutine(Invunerability());
        }
        else{
            if(!isDead){
                animator.SetTrigger("dead");
                GetComponent<PlayerController>().enabled = false;
                isDead = true;
            }
        }
    }




    void Update(){
        /*if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }*/
    }



    private IEnumerator Invunerability(){
        Physics2D.IgnoreLayerCollision(7, 8, true);

        for(int i = 0; i < numberOfFlashes; i++){
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);

            yield return new WaitForSeconds(invulnerabilityDuration/ (numberOfFlashes*2));

            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(invulnerabilityDuration/ (numberOfFlashes*2));
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

}
