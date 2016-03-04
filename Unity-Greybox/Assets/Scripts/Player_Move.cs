using UnityEngine;
using System.Collections;

public class Player_Move : MonoBehaviour {

	float distRight;
	float distForward;
	float initialX;
	float initialY;
	float initialZ;

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
		if (Input.GetKeyDown ("w"))
		{
			distForward++;
		}
		if (Input.GetKeyDown ("a"))
		{
			distRight--;
		}
		if (Input.GetKeyDown ("s"))
		{
			distForward--;
		}
		if (Input.GetKeyDown ("d"))
		{
			distRight++;
		}
		transform.position = new Vector3 (initialX + distRight, initialY, initialZ + distForward);
	}
}
