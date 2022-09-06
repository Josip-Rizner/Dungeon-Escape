using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifferentRoomDoorController : MonoBehaviour
{
    private bool isNearDoor;
    [SerializeField] Text tooltipText;
    [SerializeField] float differentRoomX;
    [SerializeField] float differentRoomy;
    [SerializeField] GameObject player;
    [SerializeField] GameObject tooltipPanel;


    // Start is called before the first frame update
    void Start()
    {
        isNearDoor = false;
        tooltipText.text = "";
        tooltipPanel.SetActive(false);
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
            tooltipText.text = "Press E to enter this door";
            isNearDoor = true;
            tooltipPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7){
            tooltipText.text = "";
            tooltipPanel.SetActive(false);
            isNearDoor = false;
        }
    }

    private void TeleportPlayerToDifferentRoom(){
        player.transform.position = new Vector3(differentRoomX, differentRoomy, -1);
    }

}
