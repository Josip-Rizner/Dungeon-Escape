using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDoorController : MonoBehaviour
{
    private bool isUnlocked, isNearDoor;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Text tooltipText;
    [SerializeField] Sprite lockedDoor, unlockedDoor;

    private KeysController keysController;

    // Start is called before the first frame update
    void Start()
    {
        isUnlocked = false;
        isNearDoor = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = lockedDoor;

        keysController = GetComponentInParent<KeysController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keysController.CheckIfAllKeysAreCollected()){
            isUnlocked = true;
            spriteRenderer.sprite = unlockedDoor;
        }

        if(isNearDoor && isUnlocked && Input.GetKeyDown(KeyCode.E)){
            tooltipText.text = "You Won";
            GetComponent<ExitDoorController>().enabled = false;
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isUnlocked){
            tooltipText.text = "Collect all keys to exit this room";
            isNearDoor = true;
        }

        if(collision.gameObject.layer == 7 && isUnlocked){
            tooltipText.text = "Press E to exit this room";
            isNearDoor = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isUnlocked){
            tooltipText.text = "";
            isNearDoor = false;
        }

        if(collision.gameObject.layer == 7 && isUnlocked){
            tooltipText.text = "";
            isNearDoor = false;
        }

    }
}
