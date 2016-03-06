using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public Vector3 inputVelocity {get; private set;}
    public float speed = 1;
	
	void Update () {
        //get the player inputted velocity
        inputVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        //move avatar after applying speed and delta time
        transform.position += inputVelocity * speed * Time.deltaTime;

        if(inputVelocity.magnitude > 0)
        {
            //get y component of the avatar's velociy and degrees
            float heading = Mathf.Atan2(inputVelocity.x, inputVelocity.z) * Mathf.Rad2Deg;
            //rotate the avatar to match their velocity
            transform.localEulerAngles = Vector3.up * heading;
        }
    }
}
