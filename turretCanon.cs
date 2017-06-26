using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretCanon : MonoBehaviour {

    turretSoundManager sound; // create a reference of the turret sound controller object

    public GameObject bullet; // store the bullet to instantiate
    public GameObject gun; // object to have the position of the instance 
    public float reloadTime = .5f; // reload time counter

    private float timeReload; // private variable used to be decremented by time

	// Use this for initialization
	void Awake () {
        timeReload = reloadTime; // initialise the variable so it's now easy to tweak in the inspector
        sound = FindObjectOfType<turretSoundManager>(); // initialise the turret sound controller 

    }	
	// Update is called once per frame
	void Update () {
        timeReload-= Time.deltaTime; // the decrementation itself
        
    if (timeReload < 0) // condition to instantiate a bullet
        {
            Instantiate(bullet, gun.transform.position, transform.rotation); // instantiate the bullet
            sound.laserSound(.5f); // call the function that play the sound still better than having the sound here 
            timeReload = reloadTime; // reset the counter
        }
	}
    // manage the collisions between this object and other objects
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bombRadius")) // what happen when this object collide with a bombRadius gameobject
        {
            Destroy(gameObject); // temporary situation 
        }

    }
}
