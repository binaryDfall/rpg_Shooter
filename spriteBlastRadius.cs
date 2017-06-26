using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteBlastRadius : MonoBehaviour {

    public float radiusFactor = 2f; // the multiplier of the localScale

	// Update is called once per frame
	void Update () {

        radiusFactor += 2f; // increase the multiplier on each frame by .2f
        transform.localScale *= radiusFactor; // set the scale of this object by the radiusFactor
	}
}
