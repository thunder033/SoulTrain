using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Soul))]
public class NPC : MonoBehaviour
{
    Soul soul;

    // Use this for initialization
    void Start()
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

    }
}