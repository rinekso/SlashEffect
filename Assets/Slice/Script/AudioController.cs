using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] clipsSlash;
    public AudioClip[] clipsHit;
    public AudioClip Dash;
    AudioSource audioSource;
    public void PlaySlash(){
        int id = Random.Range(0,clipsSlash.Length-1);
        audioSource.clip = clipsSlash[id];
        audioSource.Play();
    }
    public void PlayDash(){
        audioSource.clip = Dash;
        audioSource.Play();
    }
    public void PlayHit(){
        int id = Random.Range(0,clipsHit.Length-1);
        audioSource.clip = clipsHit[id];
        audioSource.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
