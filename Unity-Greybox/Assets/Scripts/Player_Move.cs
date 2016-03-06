using UnityEngine;
using System.Collections;

public class Player_Move : MonoBehaviour {

	/*
	Vector3 acceleration;
	Vector3 velocity;
	CharacterController charControl;
	float maxSpeed;
	*/
	float distRight;
	float distForward;
	float initialX;
	float initialY;
	float initialZ;
	float wave;

	// Use this for initialization
	void Start () {
		/*
		acceleration = Vector3.zero;
		velocity = transform.forward;
		charControl = GetComponent<CharacterController>();
		maxSpeed = 6.0f;
		*/

		distRight = 0;
		distForward = 0;
		initialX = transform.position.x;
		initialY = transform.position.y;
		initialZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

		/*
		if (Input.GetKey ("w") && Input.GetKey ("a"))
		{
			transform.forward = new Vector3 (-(Mathf.Pow(2, 0.5f) / 2.0f), 0, (Mathf.Pow(2, 0.5f) / 2.0f));
		}
		if (Input.GetKey ("a") && Input.GetKey ("s"))
		{
			transform.forward = new Vector3 (-(Mathf.Pow(2, 0.5f) / 2.0f), 0, -(Mathf.Pow(2, 0.5f) / 2.0f));
		}
		if (Input.GetKey ("s") && Input.GetKey ("d"))
		{
			transform.forward = new Vector3 ((Mathf.Pow(2, 0.5f) / 2.0f), 0, -(Mathf.Pow(2, 0.5f) / 2.0f));
		}
		if (Input.GetKey("d") && Input.GetKey ("w"))
		{
			transform.forward = new Vector3 ((Mathf.Pow(2, 0.5f) / 2.0f), 0, (Mathf.Pow(2, 0.5f) / 2.0f));
		}
		*/

		if (Input.GetKey("w"))
		{
			distForward++;
			wave++;
		}
		if (Input.GetKey("a"))
		{
			distRight--;
			wave++;
		}
		if (Input.GetKey("s"))
		{
			distForward--;
			wave++;
		}
		if (Input.GetKey("d"))
		{
			distRight++;
			wave++;
		}
		if (Input.GetKeyDown("w")) 
		{
			transform.forward = new Vector3 (0, 0, 1);
		}
		if (Input.GetKeyDown("a")) 
		{
			transform.forward = new Vector3 (-1, 0, 0);
		}
		if (Input.GetKeyDown("s")) 
		{
			transform.forward = new Vector3 (0, 0, -1);
		}
		if (Input.GetKeyDown("d")) 
		{
			transform.forward = new Vector3 (1, 0, 0);
		}

		/*
		transform.forward = velocity.normalized;

		velocity += acceleration * Time.deltaTime;
		velocity.y = 0;

		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);

		transform.position += velocity * Time.deltaTime;

		acceleration = Vector3.zero;
		*/

		transform.position = new Vector3(initialX + distRight/5, initialY + Mathf.Sin(wave*Mathf.PI/180), initialZ + distForward/5);
		transform.rotation = Quaternion.LookRotation(transform.forward);
		//transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
	}
}
