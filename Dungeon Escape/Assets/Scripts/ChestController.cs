using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    private bool isNearTheChest;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Text tooltipText;
    [SerializeField] Sprite closedChest, openedChest;

    // Start is called before the first frame update
    void Start()
    {
        isNearTheChest = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = closedChest;
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearTheChest && Input.GetKeyDown(KeyCode.E)){
            spriteRenderer.sprite = openedChest;
            GetComponent<ChestController>().enabled = false;
            tooltipText.text = "";
        }  
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7){
            isNearTheChest = true;
            tooltipText.text = "Press E to open the Chest";
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7){
            isNearTheChest = false;
            tooltipText.text = "";
        }

    }
}
