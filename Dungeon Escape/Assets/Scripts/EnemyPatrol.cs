using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform leftEdge; 
    [SerializeField] Transform rightEdge; 

    [SerializeField] Transform enemy;


    [SerializeField] float speed;
    private Vector3 initialScale;
    private bool isMovingLeft = true;


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

    private void MoveInDirection(int _direction){

        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * _direction, initialScale.y, initialScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

    private void DirectionChange(){
        isMovingLeft = !isMovingLeft;

    }

}
