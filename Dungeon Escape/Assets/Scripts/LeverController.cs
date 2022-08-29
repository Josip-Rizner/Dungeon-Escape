using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LeverController : MonoBehaviour
{
    private bool isNearTheLever, isSwithced;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Text tooltipText;
    [SerializeField] Sprite defaultLever, switchedLever;

    [SerializeField] Tilemap interactiveTilemap;

    // Start is called before the first frame update
    void Start()
    {
        isNearTheLever = false;
        isSwithced = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultLever;
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearTheLever && Input.GetKeyDown(KeyCode.E)){
            spriteRenderer.sprite = switchedLever;
            transform.position = new Vector3(transform.position.x - 0.02f,transform.position.y - 0.08f, transform.position.z);
            interactiveTilemap.GetComponent<TilemapCollider2D>().enabled = true;
            interactiveTilemap.GetComponent<Tilemap>().color = Color.white;
            GetComponent<LeverController>().enabled = false;
            tooltipText.text = "";
            isSwithced = true;
            StartCoroutine(BlocksAppearedNotification());
        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isSwithced){
            isNearTheLever = true;
            tooltipText.text = "Press E to switch the Lever";
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isSwithced){
            isNearTheLever = false;
            tooltipText.text = "";
        }

    }


    private IEnumerator BlocksAppearedNotification(){
        tooltipText.text = "Layout of the dunguen changed";
        yield return new WaitForSeconds(3);
        tooltipText.text = "";
    }
}
