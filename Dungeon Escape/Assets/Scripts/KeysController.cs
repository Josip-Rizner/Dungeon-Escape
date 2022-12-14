using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysController : MonoBehaviour
{

    public static int collectedKeys;
    private int keysNeded;

    private GameObject keyPanel;
    private Text keyCountText;

    private int numberOfChests;
    // Start is called before the first frame update
    void Start()
    {
        numberOfChests = GetComponentsInChildren<ChestController>().Length;
        keysNeded = numberOfChests;
        keyPanel = GameObject.Find("Player/Main Camera/Canvas/Panel");
        keyCountText = keyPanel.GetComponentInChildren<Text>();


        keyCountText.text = "0/" + keysNeded;
    }

    // Update is called once per frame
    void Update()
    {
        keyCountText.text = collectedKeys + "/" + keysNeded;   
    }

    public void AddKeyToTheCount(){
        collectedKeys++;
    }

    public bool CheckIfAllKeysAreCollected(){
        if(collectedKeys == keysNeded){
            return true;
        }

        return false;
    }
}
