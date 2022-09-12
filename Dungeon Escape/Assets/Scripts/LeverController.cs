using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LeverController : MonoBehaviour
{
    private bool isNearTheLever, isSwithced;
    private SpriteRenderer spriteRenderer;

    [SerializeField] bool isVisibleFromTheStart;

    [SerializeField] Sprite defaultLever, switchedLever;

    [SerializeField] Tilemap interactiveTilemap;

    [SerializeField] AudioClip leverSwitchingSound;
    // Start is called before the first frame update
    void Start()
    {
        isNearTheLever = false;
        isSwithced = false;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultLever;
        
        TooltipController.instance.hideToolip();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearTheLever && Input.GetKeyDown(KeyCode.E)){

            if(!isVisibleFromTheStart){
                spriteRenderer.sprite = switchedLever;
                transform.position = new Vector3(transform.position.x - 0.02f,transform.position.y - 0.08f, transform.position.z);
                interactiveTilemap.GetComponent<TilemapCollider2D>().enabled = true;
                interactiveTilemap.GetComponent<Tilemap>().color = Color.white;
                SoundController.instance.PlaySound(leverSwitchingSound);
                GetComponent<LeverController>().enabled = false;
                TooltipController.instance.hideToolip();
                isSwithced = true;
                StartCoroutine(BlocksAppearedNotification());
            }
            else{
                spriteRenderer.sprite = switchedLever;
                transform.position = new Vector3(transform.position.x - 0.02f,transform.position.y - 0.08f, transform.position.z);
                interactiveTilemap.GetComponent<TilemapCollider2D>().enabled = false;
                interactiveTilemap.GetComponent<Tilemap>().color = new Color(0.2830189f, 0.2336241f, 0.2336241f, 0.4666667f);;
                SoundController.instance.PlaySound(leverSwitchingSound);
                GetComponent<LeverController>().enabled = false;
                TooltipController.instance.hideToolip();
                isSwithced = true;
                StartCoroutine(BlocksAppearedNotification());
            }

        } 
    }

    void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.layer == 7 && !isSwithced){
            isNearTheLever = true;
            TooltipController.instance.showTooltip("Press E to switch the Lever");
        }

    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.layer == 7 && !isSwithced){
            isNearTheLever = false;
            TooltipController.instance.hideToolip();
        }

    }


    private IEnumerator BlocksAppearedNotification(){
        TooltipController.instance.showTooltip("Layout of the dunguen changed");
        yield return new WaitForSeconds(3);
        TooltipController.instance.hideToolip();
    }
}
