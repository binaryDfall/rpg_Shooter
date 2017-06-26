using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blastRadius : MonoBehaviour {

    CircleCollider2D col;// create a reference to the collider attached to

    public float explosionSpread  = 1f; // the rate of the explosion
    public float minradius = 1f; // the minimu radius of the colider
    public float maxRadius = 5f; // the maximum radius of the collider

	// Use this for initialization
	void Start () {
        col = GetComponent<CircleCollider2D>(); // initialise the col variable
        col.radius = minradius; // initialise the radius of the collider on the minimum radius value
	}
	
	// Update is called once per frame
	void Update () {
        explosionSpread += 1f ; // increase the rate on the delta Time
        col.radius =  explosionSpread ; // set the new radius value

        if ( col.radius == maxRadius) // what's happen when the collider reach his maximum radius
        {
            Destroy(gameObject); // destroy gameobject
        }
	}
}

