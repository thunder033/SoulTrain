using UnityEngine;
using System.Collections;

public class Player_Move : MonoBehaviour {

	float distRight;
	float distForward;
	float initialX;
	float initialY;
	float initialZ;
	Vector3 forward;
	float directionAngle;

	// Use this for initialization
	void Start () {
		distRight = 0;
		distForward = 0;
		initialX = transform.position.x;
		initialY = transform.position.y;
		initialZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("w"))
		{
			distForward++;
			transform.forward = new Vector3 (0, 0, 1);
		}
		if (Input.GetKey("a"))
		{
			distRight--;
			transform.forward = new Vector3 (-1, 0, 0);
		}
		if (Input.GetKey("s"))
		{
			distForward--;
			transform.forward = new Vector3 (0, 0, -1);
		}
		if (Input.GetKey("d"))
		{
			distRight++;
			transform.forward = new Vector3 (1, 0, 0);
		}
		transform.position = new Vector3 (initialX + distRight/5, initialY, initialZ + distForward/5);
		//transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
	}
}
