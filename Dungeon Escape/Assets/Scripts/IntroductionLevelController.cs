using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionLevelController : MonoBehaviour
{
    [SerializeField] GameObject player;

    private float playerXPosition;

    void Start(){
        player = GameObject.Find("Player");
        TooltipController.instance.hideToolip();
    }

    void Update(){
        
        playerXPosition =  player.transform.position.x;

        if(playerXPosition > -6 && playerXPosition < -3){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("Use WASD or arrow keys to move around"));
        }

        if(playerXPosition > -5 && playerXPosition < -2){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("Falling on spikes kills you instantly, avoid them!"));
        }

        if(playerXPosition > 13 && playerXPosition < 17){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("You can jump and grab ledges up to 3 blocks high"));
        }

        if(playerXPosition > 31 && playerXPosition < 34){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("Use levers to change the layouts of the rooms"));
        }

        if(playerXPosition > 52 && playerXPosition < 57){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("Keys are hidden in chests, collect all the keys to beat the level!"));
        }

        if(playerXPosition > 70 && playerXPosition < 76){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("Press space to attack"));
        }

        if(playerXPosition > 88 && playerXPosition < 91){
            Debug.Log(playerXPosition);
            StartCoroutine(ShowTip("After collecting all the keys find the Exit doors to finish the level"));
        }
    }


    private IEnumerator ShowTip(string tip){
        TooltipController.instance.showTooltip(tip);
        yield return new WaitForSeconds(5);
        TooltipController.instance.hideToolip();
    }
}
