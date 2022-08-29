using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform leftEdge; 
    [SerializeField] Transform rightEdge; 

    [SerializeField] Transform enemy;


    [SerializeField] float speed;
    [SerializeField] float delayBetweenTurningAndAttackingWhenAttacked;
    private Vector3 initialScale;
    private bool isMovingLeft = true;

    [SerializeField] Animator animator;

    [SerializeField] float idleDuration;
    private float idleTimer;

    void Start(){
        initialScale = enemy.localScale;

    }

    void Update(){

        if(isMovingLeft){
            if(enemy.position.x >= leftEdge.position.x){
                MoveInDirection(-1);
            }
            else{
                DirectionChange();
            }
        }
        else{
            if(enemy.position.x <= rightEdge.position.x){
                MoveInDirection(1);
            }
            else{
                DirectionChange();
            }
        }
    }


    void OnDisable(){
        animator.SetBool("moving", false);
    }

    private void MoveInDirection(int _direction){

        idleTimer = 0;

        animator.SetBool("moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    private void DirectionChange(){

        animator.SetBool("moving", false);

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration){
            isMovingLeft = !isMovingLeft;
        }
    }

    public void TurnWhenAttacked(){
        animator.SetBool("moving", false);
        isMovingLeft = !isMovingLeft;
        StartCoroutine(AttackDelayAfterTurningWhenAttacked());
    }



    private IEnumerator AttackDelayAfterTurningWhenAttacked(){
        GetComponentInChildren<SkeletonController>().enabled = false;
        yield return new WaitForSeconds(0.05f);
        GetComponent<EnemyPatrol>().enabled = false;
        yield return new WaitForSeconds(delayBetweenTurningAndAttackingWhenAttacked);
        GetComponentInChildren<SkeletonController>().enabled = true;
        GetComponent<EnemyPatrol>().enabled = true;
    }
}
