using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance {get; private set;} 
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this){
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound){
        source.PlayOneShot(_sound);

    }

    public void PlaySoundIfFinished(AudioClip _sound){
        if(!source.isPlaying){
            source.PlayOneShot(_sound);
        }
    }

    public void StopSound(){
        source.Stop();
    }
}
