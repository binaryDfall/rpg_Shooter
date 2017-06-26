using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour {

    Rigidbody2D rb2d; // create a referance of the rigidbody attached 
    secondarySoundManager secondaryWeaponSound; // create a reference of the sound controller that play secondary weapon sounds

    public GameObject explosion; // store the explosion object
    public float speed; // store the speed of the bomb
    public float lifeTime; // store the lifeTime of the bomb as a float value

	// Use this for initialization
	void Awake () {
        secondaryWeaponSound = FindObjectOfType<secondarySoundManager>(); // initialise the secondary weapon sound controller
        rb2d = GetComponent<Rigidbody2D>(); // initialise the rigidbody component
        StartCoroutine(destroyOnTImeC()); // start the coroutine to destroy the object after a certain time
	}
    // Update is called once per frame
    void FixedUpdate () {
        Vector2 dir = transform.up; // store the direction of the bomb
        rb2d.AddForce(dir * speed); // move the bomb , addForce add a progressiv force so the bomb movement will be realistic
	}
    // enum that manage the life time of the bomb
    private IEnumerator destroyOnTImeC()
    {
        yield return new WaitForSeconds(lifeTime); // counter thzat is called on awake
        Instantiate(explosion, transform.position, transform.rotation); // create the explosion
        secondaryWeaponSound.bombExplosionSound(.5f); // play the sound of the explosion
        Destroy(gameObject); // destroy the object
    }
    // function that manage the collisions between bomb and other object
    public void OnCollisionEnter2D ( Collision2D other)
    {
       if (other.gameObject.CompareTag("solid") || other.gameObject.CompareTag("turret") || other.gameObject.CompareTag("coin") || other.gameObject.CompareTag("turretLaser")) // severals conditions to trigger an explosion
        {
            Instantiate(explosion, transform.position, transform.rotation); // create the explosion
            secondaryWeaponSound.bombExplosionSound(.5f); // play the sound of the explosion
            Destroy(gameObject); // destroy the object
            // maybe explode should be better 
        }
    }

}
