using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance {get; private set;} 

    [SerializeField] AudioClip looseSound;
    [SerializeField] AudioClip winningSound;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void QuitRequest(){
        Application.Quit();
    }

    public void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    public void LoadNextLevel(){
        SceneManager.LoadScene(PrevisousSceneController.previousScene + 1);
    }


    public void LoadSameLevel(){
        SceneManager.LoadScene(PrevisousSceneController.previousScene);
    }

    public int GetSceneIndex(){
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadLooseScene(){
        SceneManager.LoadScene("LooseScreen");
        SoundController.instance.PlaySound(looseSound);
        KeysController.collectedKeys = 0;
    }

    public void LoadWinningScene(){
        SceneManager.LoadScene("WinningScreen");
        SoundController.instance.PlaySound(winningSound);
    }

}
