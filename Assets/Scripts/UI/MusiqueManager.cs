using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiqueManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] myAudioClips;
    private AudioSource myAudioSource;
    private List<string> theMusics = new List<string>();

    private void Awake() 
    {
        myAudioSource=this.GetComponent<AudioSource>();
        myAudioSource.clip=myAudioClips[(int)Random.Range(0,theMusics.Count)];
        myAudioSource.volume=0.5f;
    }
    private void Start() 
    {
        Play();
    }
    
     private void Play()
    {
        if(!myAudioSource.isPlaying)
            myAudioSource.Play();
            myAudioSource.loop=true;
    }

    private void Stop()
    {
        if(myAudioSource.isPlaying)
        myAudioSource.Stop();
    }

    
}
