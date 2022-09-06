using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitDoorController : MonoBehaviour
{
    private bool isUnlocked, isNearDoor, isAlreadyChecked;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Text tooltipText;
    [SerializeField] GameObject tooltipPanel;
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

        tooltipText.text = "";
        tooltipPanel.SetActive(false);
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
            tooltipPanel.SetActive(true);
            tooltipText.text = "Collect all keys to unlock this door";
            isNearDoor = true;
        }

        if(collision.gameObject.layer == 7 && isUnlocked){
            tooltipPanel.SetActive(true);
            tooltipText.text = "Press E to exit this room";
            isNearDoor = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isUnlocked){
            tooltipText.text = "";
            tooltipPanel.SetActive(false);
            isNearDoor = false;
        }

        if(collision.gameObject.layer == 7 && isUnlocked){
            tooltipText.text = "";
            tooltipPanel.SetActive(false);
            isNearDoor = false;
        }

    }

    private IEnumerator KeysCollectedNotification(){
        tooltipPanel.SetActive(true);
        tooltipText.text = "All the Keys are found, exit door is unlocked!.";
        yield return new WaitForSeconds(3);
        tooltipText.text = "";
        tooltipPanel.SetActive(false);
    }
}
