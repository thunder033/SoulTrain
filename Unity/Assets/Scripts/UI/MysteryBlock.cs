using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MysteryBlock : MonoBehaviour {

    public Text titleText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMystery(Mystery mystery) {
        titleText.text = mystery.Name;
    }
}
