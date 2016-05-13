using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MysteryBlock : MonoBehaviour {

    public Text titleText;
    private Mystery mystery;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMystery(Mystery mystery) {
        titleText.text = mystery.Name;
        this.mystery = mystery;
    }

    public Mystery GetMystery() {
        return mystery;
    }
}
