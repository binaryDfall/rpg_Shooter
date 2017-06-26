using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    private  GameObject player; // create a variable to store the player position 
    private Vector3 offset; // store the offset distance between the camera and the player

	// Use this for initialization
	void Start () {
        player = UnityEngine.GameObject.FindGameObjectWithTag("Player"); // set the player variable to the current player inGame
        offset = transform.position - player.transform.position;     // Calculate and store the offset value by getting the distance between the player's position and camera's position 
         
    }	
	// LateUpdate is called just after each Update
	void LateUpdate () {
        transform.position = player.transform.position + offset ; // calculate the position of the camera 

	}
}
