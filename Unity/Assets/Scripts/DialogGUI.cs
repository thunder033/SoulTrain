using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent (typeof (Soul))]
public class DialogGUI : MonoBehaviour {
    Soul soul;
    DialogLine currentLine;

    public Canvas canvas;

	// Use this for initialization
	void Start () {
        soul = GetComponent<Soul>();
        soul.HearResponse = new Conversation.ResponseHandler(Respond);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            soul.Speak(currentLine.GetResponses()[0]);
        }
    }

    public virtual void Respond(Conversation convo)
    {
        currentLine = convo.CurrentLine;
        GameObject.Find("DialogBox").GetComponent<Text>().text = currentLine.speaker.name + ": " + currentLine.text;
    }
}
