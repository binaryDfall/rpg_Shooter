using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    soundManager sound; // create a ref to the sound Manager
    Rigidbody2D rb2d; // create a ref to the rigidbody2D component 
    new AudioSource audio; // this audioSource is needed to play sounds based on player inputs

    public AudioClip basicLaser; // the basicLaser sound of the ship
    public GameObject gun; //  store from where the bullet is shooted
    public GameObject bullet; // store the bullet
    public GameObject bomb; // store the bomb gameobject
    public float movementSpeed; // store the speed of the ship
    public float shootingInterval = 1; // time before shooting again
    public float shootingBombInterval = 1; // time before shooting secondary weapon again
    public int money = 0; // the gold that the player have in stock
    public int hull; // represent the current life of the ship
    public int maxHull = 10; // store the maximum value of the hull
    public float shield; // represent the number of hit that the shield can take 
    public float shieldReload = 5f; // time to reloadthe shield
    public float accelerationSpeed ; // the boostSpeed;
    public float normalSpeed; // the normal speed
    public int boostFuel; // store the current fuel available
    public int maxBosstFuel = 50; // store the maximum fuel value
    public int nbrBomb; // store the current number of bomb available
    public int nbrBombMax = 3; // store the maximum bomb value

    private float accelerationFactor = 1.5f; // the multiplier of the acceleration speed 
    private bool canShootBomb = true; // can the ship shoot a bomb or not while input pressed ?
    private bool canShoot = true; // can the ship shoot or not while input pressed ?
    private float intervalShooting; // counter to decrement shootingInterval 
    private float intervalShootingBomb; // counter to decrement shootingBombInterval
    

	// Use this for initialization
	void Awake () {
        sound = FindObjectOfType<soundManager>(); // initialise the soundManager that play only sounds without input ( hit , collect, etc ) 
        audio = GetComponent<AudioSource>(); // initialise the audioSourceComponent
        rb2d = GetComponent<Rigidbody2D>(); // initialise the variable rb2d
        intervalShooting = shootingInterval; // initialise the private variable
        intervalShootingBomb = shootingBombInterval; // initialise the private variable
        hull = maxHull; // initialise the max life of the ship
        shield = 1; // initialise the max shield value
        accelerationSpeed = movementSpeed * accelerationFactor; // initialise the acceleration speed by timing the movement speed by accelerationFactor
        normalSpeed = movementSpeed; // initialise the normal speed on the movement speed
        boostFuel = maxBosstFuel; // initialise the boostfuel on the maximum fuel value
        nbrBomb = nbrBombMax; // initialise the number of bombs on the maximum bomb value

	}
    // use this for physics incrementation
    void FixedUpdate() {
        float h = Input.GetAxisRaw( "Horizontal" ); // float that handle movement on x axis ( input.x ) 
        float v = Input.GetAxisRaw( "Vertical" ); // float that handle movement on y axis   ( input.y )
        float boostSpeed = Input.GetAxisRaw( "Fire2" ); // float that handle the acceleration boost on all axis 
        Vector2 move = new Vector2( h, v ); // storing x and y value in a vector2

        if ( boostSpeed == 1 && boostFuel > 0 ) // condition to have a boost
        {
            movementSpeed = accelerationSpeed; // convert the normal speed to a temporary fastest speed
            boostFuel--; // decrement the boosfuel value

                } else if ( boostSpeed == 0 ) // if boost input is not pressed
                    {
                        movementSpeed = normalSpeed; // reset the speed value
                    }

        rb2d.velocity = ( Vector3 )move * movementSpeed; // move the ship when inputs are pressed timed by the speed of the ship
        move.Normalize(); // avoid too fast movement when moore than 1 input are pressed
        rb2d.freezeRotation = true; // avoid the ship to rotate on z axis 
        rotation(); // call the rotation function 

    }
    // Update is called once per frame
    void Update() {
        float fire = Input.GetAxisRaw( "Fire1" ); // store the input to fire
        float fireBomb = Input.GetAxisRaw( "Fire3" ); // store the input to fire a bomb

        // prints to debug the rpg values ( maybe make a function for this later ) 
        print( "il" + " " + " reste" + " " + hull + " " + "hp" );
        print( "tu " + " " + "à" + " " + money + " " + "d' or" );
        print( "il reste " + " " + shield + "  " + " de bouclier energetique" );
        print( "il reste " + " " + boostFuel +  " " + " de fuel" );
        ////////////////////<=============>\\\\\\\\\\\\\\\\\

        intervalShooting -= Time.deltaTime; // decrement the counter by the deltaTime
        intervalShootingBomb -= Time.deltaTime; // decrement the counter by the deltaTime
        
        if ( intervalShootingBomb < 0 && nbrBomb > 0 ) // condition needed to be able to shoot a bomb
        {
            canShootBomb = true; // yes you can bomb the world
            intervalShootingBomb = shootingBombInterval; // reset the counter
        } 

        if ( fireBomb == 1 && canShootBomb ) // condition to be able to shoot a bomb
        {
            shootBomb(); // call the function taht instantiate a bomb
            intervalShootingBomb = shootingBombInterval; // reset the counter
            nbrBomb--; // decrement the number of bombs
            canShootBomb = false; // impossible to shot an infinity of bombs ( or just the maxbomb value )
        }

        if ( fire == 1 && canShoot ) // conditions to be able to shoot a bullet
        {
            shoot(); // call the function to shoot a bullet
            intervalShooting = shootingInterval; // reset the counter
            canShoot = false; // impossible to shoot an infinity of projectiles 

                 } else if ( fire == 0 && canShoot ) // condition to reset the possibility to shoot
                    {
                        canShoot = true; // yes you can
                    }

        if ( intervalShooting < 0 ) // when the counter reach his goal
            {
                canShoot = true; // it's possible to shoot again
                intervalShooting = shootingInterval; // reset the counter
            }
    }
    // function that instantiate a bullet
    public void shoot ()
    {
        Instantiate( bullet, gun.transform.position, transform.rotation ); // create an instance of the object bullet
        audio.PlayOneShot( basicLaser , .5f ); // play the basic laser sound
    }
    // function that instantiate a bomb
    public void shootBomb()
    {
        Instantiate( bomb, gun.transform.position, transform.rotation ); // create an instance of the bomb object
    }
    // manage the collision between player and other objects
    public void OnCollisionEnter2D ( Collision2D other )
    {
        if ( other.gameObject.CompareTag( "turretLaser" ) || other.gameObject.CompareTag( "solid" )) // several conditions that call the hit () ;
        {
            if ( shield == 1 ) // if the shield is on
            {
                shieldHit(); // decrement the shield
            }
            else { // if the shield is off
                 hullHit(); // decrement the hull           
            }
        }

        if ( other.gameObject.CompareTag ( "coin" )) // condition to increment the money value
        {
            money += 10; // incrementation in itelf
        }

        if ( other.gameObject.CompareTag ("coinx10" )) // condition to increment the money value X 100 !! 
        {
            money += 100; // incrementation in itelf
        }
        
        if ( other.gameObject.CompareTag ( "life" )) // condition to increment the hull value
        {
            if ( hull == maxHull ) // if the life of the ship is at his maximum value
            {
                hull = maxHull; // then it don't increment
                } else // if the life of the ship is below the maximum value
                    {
                        hull++; // the ship gain hull points
                    }               
        }
    }
    // what's happen when the player shield is hit by a bullet 
    public void shieldHit()
    {
        sound.shieldDestroySound( .5f ); // play the destroy shield  sound
        shield -= 1; // decrement the shield value

        if ( shield <= 0 ) // condition to reload the shield
        {
            StartCoroutine( reloadShieldC() ); // coroutine that reload the shield
        }
    }
    // what's happen when the player hull is hit by a bullet 
    public void hullHit()
    { 
        sound.playerHitSound( .5f ); // play the destroy hull sound
        hull--; // decrement the hull value
                  
        if ( hull == 0 ) // condition of death
        {
            Destroy( gameObject ); // this is maybe not the good way 
        }
    }
    // enum that manage that reload the shield based on shieldReload variable
    IEnumerator reloadShieldC ()
    {
        yield return new WaitForSeconds( shieldReload ); // time before reloading
        shield = 1; // reset the shield value to 1
    }

    // function to manage the rotation of the ship when input are pressed
    public void rotation()
    {
        float h = Input.GetAxisRaw( "Horizontal" ); // float that handle movement on x axis ( input.x ) 
        float v = Input.GetAxisRaw( "Vertical" ); // float that handle movement on y axis   ( input.y )

         if ( h < 0 ) // condition to execute a left rotation
         {
            transform.rotation = Quaternion.Euler( 0f, 0f, 90f ); // left rotation
                } else if ( h > 0 ) // condition to execute a right rotation
                    {
                        transform.rotation = Quaternion.Euler( 0f, 0f, -90f ); // right rotation
                    }

        if ( v < 0 ) // condition to execute a top rotation
        {
            transform.rotation = Quaternion.Euler( 0f, 0f, 180f ); // top rotation 
                } else if ( v > 0 ) // condition to execute a down rotation
                    {
                        transform.rotation = Quaternion.Euler( 0f, 0f, 0f ); // down rotation
                    }

        if ( h < 0 && v < 0 ) // condition to execcute a down/left rotation
        {
            transform.rotation = Quaternion.Euler( 0f, 0f, 135f ); // down/left rotation
                } else if ( h > 0 && v < 0 ) // condition to execute a down/right rotation
                    {
                        transform.rotation = Quaternion.Euler( 0f, 0f, -135f ); // down/right rotation
                    }

        if ( h < 0 && v > 0 ) // condition to execute a top/left rotation
        {
            transform.rotation = Quaternion.Euler( 0f, 0f, 45f ); // top/left rotation
                } else if ( h > 0 && v > 0 ) // condition to execute a top/right rotation
                    {
                        transform.rotation = Quaternion.Euler( 0f, 0f, -45f ); // top/right rotation
                    }
    }
}

