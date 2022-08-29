using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    private bool isNearTheChest, isOpened;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Text tooltipText;
    [SerializeField] Sprite closedChest, openedChest;

    private KeysController keysController;

    // Start is called before the first frame update
    void Start()
    {
        isNearTheChest = false;
        isOpened = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = closedChest;

        keysController = GetComponentInParent<KeysController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearTheChest && Input.GetKeyDown(KeyCode.E)){
            spriteRenderer.sprite = openedChest;
            keysController.AddKeyToTheCount();
            GetComponent<ChestController>().enabled = false;
            tooltipText.text = "";
            isOpened = true;
            StartCoroutine(KeyFoundNotification());
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isOpened){
            isNearTheChest = true;
            tooltipText.text = "Press E to open the Chest";
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isOpened){
            isNearTheChest = false;
            tooltipText.text = "";
        }

    }


    private IEnumerator KeyFoundNotification(){
        tooltipText.text = "You found a Key!";
        yield return new WaitForSeconds(3);
        tooltipText.text = "";
    }
}
