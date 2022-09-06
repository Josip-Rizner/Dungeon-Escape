using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    private bool isNearTheChest, isOpened;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Text tooltipText;

    [SerializeField] GameObject tooltipPanel;
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
        tooltipText.text = "";
        tooltipPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearTheChest && Input.GetKeyDown(KeyCode.E)){
            spriteRenderer.sprite = openedChest;
            keysController.AddKeyToTheCount();
            SoundController.instance.PlaySound(chestOpeningSound);
            GetComponent<ChestController>().enabled = false;
            tooltipText.text = "";
            tooltipPanel.SetActive(false);
            isOpened = true;
            StartCoroutine(KeyFoundNotification());
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isOpened){
            isNearTheChest = true;
            tooltipText.text = "Press E to open the Chest";
            tooltipPanel.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isOpened){
            isNearTheChest = false;
            tooltipText.text = "";
            tooltipPanel.SetActive(false);
        }

    }


    private IEnumerator KeyFoundNotification(){
        tooltipText.text = "You found a Key!";
        tooltipPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        tooltipText.text = "";
        tooltipPanel.SetActive(false);
    }
}
