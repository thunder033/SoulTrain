using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    Vector3 velocity;

    public float speed;
    float maxSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        velocity = new Vector3();

        velocity.y = 0;
        velocity.x += Input.GetAxis("Horizontal") * Time.deltaTime;
        velocity.z += Input.GetAxis("Vertical") * Time.deltaTime;

        transform.position += velocity;
        float heading = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

        if(velocity.magnitude > 0)
        {
            transform.localEulerAngles = new Vector3(0, heading, 0);
        }
        
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, heading, 0), 5 * Time.deltaTime);
    }
}
