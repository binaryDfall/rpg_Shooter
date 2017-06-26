using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifePick : MonoBehaviour {

    soundManager sound; // create a reference of the sound manager 

	// Use this for initialization
	void Start () {
        sound = FindObjectOfType<soundManager>(); // initialise the sound Manager variable

    }

    public void OnCollisionEnter2D ( Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            sound.pickUpSound(.5f);
            Destroy(gameObject);
    }
}
