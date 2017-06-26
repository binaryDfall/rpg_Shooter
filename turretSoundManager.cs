using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretSoundManager : MonoBehaviour {

    new AudioSource audio; // store the audioSource component

    public AudioClip laser; // store the variable to the laser sound

	// Use this for initialization
	void Awake () {
        audio = GetComponent<AudioSource>(); // initialise the audioSource component
    }

    public void laserSound( float volume)
    {
        if (!audio.isPlaying) // with this its impossbile to duplicate the sound if there is more than 1 object that play 
        {
            audio.PlayOneShot(laser, volume);  // play the sound of the turret laser 
        }
    }
}
