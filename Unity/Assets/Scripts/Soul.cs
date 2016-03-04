using UnityEngine;
using System;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

public class Soul : MonoBehaviour, IStoryElement {

    Conversation defaultConvo;
    Conversation activeConvo;

    List<Conversation> conversations {get; set;}
    public Conversation.ResponseHandler HearResponse;

    [SerializeField]
    private string _id;

    public string id
    {
        get { return _id; }
        set { _id = value; }
    }

    // Use this for initialization
    void Start () {
        conversations = new List<Conversation>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public Conversation Converse(Soul soul)
    {
        Conversation nextConvo;
        int index = 0;

        do
        {
            if(index == conversations.Count)
            {
                nextConvo = defaultConvo;
                break;
            }
            nextConvo = conversations[index++];
        }
        while (nextConvo.isComplete || !nextConvo.HasParticipant(soul));

        nextConvo.Start();
        return nextConvo;
    }

    public void JoinConversation(Conversation convo)
    {
        activeConvo = convo;
    }

    public void ExitConversation(bool endConversation)
    {
        if (endConversation && activeConvo != null)
        {
            activeConvo.End();
        }
        else
        {
            activeConvo = null;
        }
    }

    public void Speak(DialogLine line)
    {
        activeConvo.Respond(line);
    }

    public void AddConversation(Conversation convo)
    {
        conversations.Add(convo);
    }
}

/// <summary>
/// The Soul Loader links instances of Souls defined in the scene to those defined in the data file
/// </summary>
public class SoulLoader : DataLoader
{
    Soul[] souls;

    public override string fileName { get; protected set; } = "Souls.json";
    public override Type DataType { get; protected set; } = typeof(Soul);

    public SoulLoader()
    {
        souls = UnityEngine.Object.FindObjectsOfType<Soul>();
    }

    public override object FromJSON(JSONNode data)
    {
        foreach(Soul soul in souls)
        {
            string id = data["id"].Value;
            if (soul.id == id)
            {
                AddInstance(soul);
                return soul;
            }
        }

        Debug.LogWarning("No Soul was found with ID " + data["id"]);

        return null;
    }
}