using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    private bool isNearTheChest, isOpened;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite closedChest, openedChest;

    private KeysController keysController;

    [SerializeField] AudioClip chestOpeningSound;

    // Start is called before the first frame update
    void Start()
    {
        isNearTheChest = false;
        isOpened = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = closedChest;

        keysController = GetComponentInParent<KeysController>();
        TooltipController.instance.hideToolip();

    }

    // Update is called once per frame
    void Update()
    {
        if(isNearTheChest && Input.GetKeyDown(KeyCode.E)){
            spriteRenderer.sprite = openedChest;
            keysController.AddKeyToTheCount();
            SoundController.instance.PlaySound(chestOpeningSound);
            GetComponent<ChestController>().enabled = false;
            TooltipController.instance.hideToolip();
            isOpened = true;
            StartCoroutine(KeyFoundNotification());
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isOpened){
            isNearTheChest = true;
            TooltipController.instance.showTooltip("Press E to open the Chest");
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isOpened){
            isNearTheChest = false;
            TooltipController.instance.hideToolip();
        }

    }


    private IEnumerator KeyFoundNotification(){
        TooltipController.instance.showTooltip("You found a Key!");
        yield return new WaitForSeconds(3);
        TooltipController.instance.hideToolip();
    }
}
