using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primarySoundManager : MonoBehaviour {

    new AudioSource audio; // create a ref of the audioSource attached 

    public AudioClip basicLaser; // sound of the basic laser

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}

    public void basicLaserSound ( float volume)
    {
        audio.PlayOneShot(basicLaser, .5f);
    }
}
