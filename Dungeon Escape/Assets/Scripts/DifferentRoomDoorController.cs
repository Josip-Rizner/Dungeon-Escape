using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifferentRoomDoorController : MonoBehaviour
{
    private bool isNearDoor;
    [SerializeField] float differentRoomX;
    [SerializeField] float differentRoomy;
    [SerializeField] GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        isNearDoor = false;
        TooltipController.instance.hideToolip();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearDoor && Input.GetKeyDown(KeyCode.E)){
            TeleportPlayerToDifferentRoom();
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7){
            isNearDoor = true;
            TooltipController.instance.showTooltip("Press E to enter this door");
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7){
            TooltipController.instance.hideToolip();
            isNearDoor = false;
        }
    }

    private void TeleportPlayerToDifferentRoom(){
        player.transform.position = new Vector3(differentRoomX, differentRoomy, -1);
    }

}
