using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Soul))]
public class NPC : MonoBehaviour
{
    Soul soul;

    // Use this for initialization
    void OnEnable()
    {
        soul = GetComponent<Soul>();
        soul.HearResponse = new Conversation.ResponseHandler(Respond);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respond(Conversation convo)
    {
        var responses = convo.CurrentLine.GetResponses(soul);

        if(convo.CurrentLine.speaker != soul)
        {
            if (responses.Count > 0)
            {
                soul.Speak(responses[0]);
            }

            if (convo.CurrentLine.IsLastLine)
            {
                soul.ExitConversation(true);
            }
        }
        
    }
}