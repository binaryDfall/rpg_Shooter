using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour {

    Rigidbody2D rb2d; // store the rigidbody2D in a variable

    public float bulletSpeed; // the speed of the bullet

	// Use this for initialization ( still better than Start() )
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>(); // initialise the rigidbody2D;
	}

    // this code is called once after the awake function
    void Start () {
        StartCoroutine(destroyOnTimeC()); // call the coroutine to destroy the bullet after a certain time
    }

    // use this for physics incrementation
    void FixedUpdate () {
    Vector2 dir = -transform.up; // store the direction of the bullet 
    rb2d.velocity = (Vector3) dir * bulletSpeed; // physics of the bullet
	}

    // coroutine to destroy the object passing a certain time 
    IEnumerator destroyOnTimeC()
    {
        yield return new WaitForSeconds(1f); // time before the object get destroyed
        Destroy(gameObject); // destroy the object
    }
    // manage the collisions of the bullet
    public void OnCollisionEnter2D (Collision2D other)
    {
            Destroy(gameObject); // destroy
    }
}
