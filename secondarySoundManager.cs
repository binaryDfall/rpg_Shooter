using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondarySoundManager : MonoBehaviour {

    new AudioSource audio; // create a reference of the audioSource attached

    public AudioClip bombLaunch; // sound that play when the bomb is launched
    public AudioClip bombExplosion; // sound that play when the bomb explode 

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>(); // initialise the audioSource component
	}
    // library of function that play secondary weapon sounds 
    public void bombLaunchSound ( float volume )
    {
        audio.PlayOneShot(bombLaunch, volume);
    }
    //\\
    public void bombExplosionSound( float volume)
    {
        audio.PlayOneShot(bombExplosion, volume);
    }
}
