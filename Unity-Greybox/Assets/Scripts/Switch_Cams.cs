using UnityEngine;
using System.Collections;

public class Switch_Cams : MonoBehaviour {

	//find cameras
	GameObject fixedCam;
	GameObject behindCam;

	// Use this for initialization
	void Start () {
		fixedCam = GameObject.FindGameObjectWithTag("Fixed_Cam");
		behindCam = GameObject.FindGameObjectWithTag("Behind_Cam");

		//make the fixed default
		fixedCam.SetActive(true);
		behindCam.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		//switch between cameras by pressing "1"

		if (Input.GetKeyDown("1")) 
		{
			fixedCam.SetActive(!fixedCam.activeSelf);
			behindCam.SetActive(!behindCam.activeSelf);
		}

		//transform.position = new Vector3 (transform.position.x, 3, transform.position.z);
	}
}
