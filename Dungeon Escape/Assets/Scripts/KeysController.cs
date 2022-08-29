using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysController : MonoBehaviour
{

    public int collectedKeys{ get; private set; }
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
        
    }

    public void AddKeyToTheCount(){
        collectedKeys++;
    }

    public bool CheckIfKeysAreCollected(){
        if(collectedKeys == keysNeded){
            return true;
        }

        return false;
    }
}
