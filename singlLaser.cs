using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singlLaser : MonoBehaviour {

    Rigidbody2D rb2d; // store the ref 


    public float bulletSpeed; // store the speed of the bullet

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>(); // initialise the variable rb2d

    }
    // use this for calling things Once
    void Start () {
        StartCoroutine(destroyOnTimeC()); // call the enum to destroy the bullet on a certain time

        Vector2 dir = transform.up; // store the direction of the bullet
        rb2d.velocity =  dir * bulletSpeed; // set the velocity of the bullet
        rb2d.velocity.Normalize(); // normalize the magnitude of the velocity 
    }
    // use this to manage the collisions
    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("solid") || other.gameObject.CompareTag("turret") || other.gameObject.CompareTag("turretLaser") || other.gameObject.CompareTag("coin") || other.gameObject.CompareTag("bomb") || other.gameObject.CompareTag("bombRadius")) // several collisions that leads to the destruction of this object
        {
            Destroy(gameObject); // destroy the object     
        }
    }
    // coroutine to destroy the object passing a certain time 
    IEnumerator destroyOnTimeC () 
    {
        yield return new WaitForSeconds(1f); // time before the object get destroyed
        Destroy(gameObject); // destroy the object
    }
}
