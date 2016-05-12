using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MysteryPage : MonoBehaviour, IPage {

    Mystery mystery;
    Journal journal;

    public Text mysteryTitle;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnLoad(Journal journal) {
        this.journal = journal;
    }

    public void SetMystery(Mystery mystery) {
        this.mystery = mystery;

        mysteryTitle.text = mystery.Name;
    }
}
