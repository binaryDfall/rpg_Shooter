using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinX10 : MonoBehaviour {

    soundManager sound; // create a reference of the soundManager GameObject

    // Use this for initialization
    void Awake()
    {
        sound = FindObjectOfType<soundManager>(); // initialise the soundManager variable
    }
    // manage the collision between this object and other object
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) // if this object collide with the player
        {
            sound.pickUpSound(.5f); // play the sound of the pick up
            Destroy(gameObject); // then destroy this object
        }
    }
}