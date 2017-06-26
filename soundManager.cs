using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

    new AudioSource audio; // store the audioSource component

    public AudioClip playerHit; // store the playerHit sound as a variable
    public AudioClip pickUp; // store the pick up sound variable ( subject to changes ) 
    public AudioClip shieldDestroy; // store the destroy of the shield

	// Use this for initialization
	void Awake () {
        audio = GetComponent<AudioSource>(); // initialise the audioSource component
		
	}
     // library of functions that play each audioclip
    public void playerHitSound ( float volume ) 
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(playerHit, volume);
        }       
    }

    public void pickUpSound ( float volume)
    {
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(pickUp, volume);
        }        
    }

    public void shieldDestroySound ( float volume)
    {
        audio.PlayOneShot(shieldDestroy, volume);
    }
}
