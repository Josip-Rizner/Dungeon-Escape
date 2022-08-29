using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysController : MonoBehaviour
{

    public static int collectedKeys;
    [SerializeField] int keysNeded;
    [SerializeField] Text keyCountText;
    // Start is called before the first frame update
    void Start()
    {
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
