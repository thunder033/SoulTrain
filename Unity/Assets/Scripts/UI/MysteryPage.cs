using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MysteryPage : MonoBehaviour, IPage {

    Mystery mystery;
    Journal journal;

    public Text mysteryTitle;
    public Text clueText;

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

        clueText.text = "";
        Clue clue = mystery.FirstClue;
        do {
            clueText.text += clue.Name + ": " + clue.Text + "\n\n";
            clue = clue.NextClue;
        } while (clue != null && clue.Discovered);

        if(clue != null && !clue.Discovered) {
            clueText.text += clue.Hint;
        }
    }
}
