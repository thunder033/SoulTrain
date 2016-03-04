using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

[RequireComponent (typeof (Soul))]
public class DialogGUI : MonoBehaviour {
    Soul soul;
    DialogLine currentLine;
    Conversation activeConvo;
    List<Text> responses;

    public Text response;
    public Canvas canvas;
    public Image responsePointer;

    int selectedResponse = 0;
    
	// Use this for initialization
	void OnEnable () {
        soul = GetComponent<Soul>();
        soul.HearResponse = new Conversation.ResponseHandler(HearDialog);
        responses = new List<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        GameObject.Find("DialogBox").GetComponent<Text>().enabled = activeConvo != null;

        if (Input.GetKeyDown(KeyCode.DownArrow) && selectedResponse + 1 < responses.Count)
        {
            selectedResponse++;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && selectedResponse > 0)
        {
            selectedResponse--;
        }

        if(responses.Count > 0)
        {
            responsePointer.enabled = true;
            Vector2 anchorPos = responses[selectedResponse].rectTransform.anchoredPosition;
            Rect textRect = responses[selectedResponse].rectTransform.rect;
            responsePointer.rectTransform.anchoredPosition = new Vector2(anchorPos.x - (textRect.width/2) - 15, anchorPos.y);
        }
        else
        {
            responsePointer.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentLine.IsLastLine)
            {
                activeConvo = null;
                soul.ExitConversation(true);
            }
            else if(currentLine.IsMonologue)
            {
                activeConvo.Continue();
            }
            else
            {
                Respond();
            }
            
        }
    }

    public void Respond()
    {
        foreach (Text response in responses)
        {
            Destroy(response.gameObject);
        }
        responses.Clear();

        soul.Speak(currentLine.GetResponses(soul)[selectedResponse]);
        selectedResponse = 0;
    }

    public virtual void HearDialog(Conversation convo)
    {
        activeConvo = convo;
        currentLine = convo.CurrentLine;
        GameObject.Find("DialogBox").GetComponent<Text>().text = currentLine.speaker.name + ": " + currentLine.text;

        Vector3 responsePosition = new Vector3(50, 200);
        int index = 1;
        foreach (DialogLine line in currentLine.GetResponses(soul))
        {
            //Text option = canvas.gameObject.AddComponent<Text>();
            //option.text = line.text;

            //Font Arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            //option.font = Arial;
            //option.material = Arial.material;

            GameObject option = (GameObject)Instantiate(Resources.Load("Prefabs/Response"), new Vector3(), Quaternion.identity);

            Text text = option.GetComponent<Text>();
            text.text = "[" + index++ + "] " + line.text;
            option.transform.SetParent(canvas.transform, false);
            responses.Add(text);

            Rect textRect = text.rectTransform.rect;
            text.rectTransform.position = new Vector3(responsePosition.x + textRect.width/2, responsePosition.y);

            responsePosition = new Vector3(responsePosition.x, responsePosition.y - 50);
        }
    }
}
