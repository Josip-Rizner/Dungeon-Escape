using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDoorController : MonoBehaviour
{
    private bool isUnlocked, isNearDoor, isAlreadyChecked;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite lockedDoor, unlockedDoor;
    [SerializeField] AudioClip unlockingTheMainDoorSound;
    [SerializeField] AudioClip openingDoorSound;

    private KeysController keysController;

    // Start is called before the first frame update
    void Start()
    {
        isUnlocked = false;
        isNearDoor = false;
        isAlreadyChecked = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = lockedDoor;

        keysController = GetComponentInParent<KeysController>();

        TooltipController.instance.hideToolip();
    }

    // Update is called once per frame
    void Update()
    {
        if(keysController.CheckIfAllKeysAreCollected() && !isAlreadyChecked){
            isUnlocked = true;
            spriteRenderer.sprite = unlockedDoor;
            SoundController.instance.PlaySound(unlockingTheMainDoorSound);
            StartCoroutine(KeysCollectedNotification());
            isAlreadyChecked = true;
        }

        if(isNearDoor && isUnlocked && Input.GetKeyDown(KeyCode.E)){
            SoundController.instance.PlaySound(openingDoorSound);
            GetComponent<ExitDoorController>().enabled = false;
            PrevisousSceneController.previousScene = SceneController.instance.GetSceneIndex();
            SceneController.instance.LoadWinningScene();
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isUnlocked){
            TooltipController.instance.showTooltip("Collect all keys to unlock this door");
        }

        if(collision.gameObject.layer == 7 && isUnlocked){
            TooltipController.instance.showTooltip("Press E to exit this room");
            isNearDoor = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isUnlocked){
            TooltipController.instance.hideToolip();
            isNearDoor = false;
        }

        if(collision.gameObject.layer == 7 && isUnlocked){
            TooltipController.instance.hideToolip();
            isNearDoor = false;
        }

    }

    private IEnumerator KeysCollectedNotification(){
        TooltipController.instance.showTooltip("All the Keys are found, exit door is unlocked!.");
        yield return new WaitForSeconds(3);
        TooltipController.instance.hideToolip();
    }
}
