using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent (typeof (Soul))]
public class DialogGUI : MonoBehaviour {
    Soul soul;
    DialogLine currentLine;
    List<Text> responses;

    public Text response;
    public Canvas canvas;

	// Use this for initialization
	void OnEnable () {
        soul = GetComponent<Soul>();
        soul.HearResponse = new Conversation.ResponseHandler(Respond);
        responses = new List<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            foreach (Text response in responses)
            {
                
            }
            responses.Clear();

            soul.Speak(currentLine.GetResponses()[0]);
        }
    }

    public virtual void Respond(Conversation convo)
    {
        currentLine = convo.CurrentLine;
        GameObject.Find("DialogBox").GetComponent<Text>().text = currentLine.speaker.name + ": " + currentLine.text;

        Vector3 responsePosition = new Vector3(-100, -250);
        foreach (DialogLine line in currentLine.GetResponses(soul))
        {
            Text option2 = new Text();

            Text option = canvas.gameObject.AddComponent<Text>();
            option.text = line.text;

            Font Arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            option.font = Arial;
            option.material = Arial.material;

            //GameObject option = (GameObject)Instantiate(response, responsePosition, Quaternion.identity);
            //option.GetComponent<Text>().text = line.text;
            responses.Add(option);

            responsePosition = new Vector3(responsePosition.x, responsePosition.y - 50);
        }
    }
}
