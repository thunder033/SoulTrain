using SimpleJSON;
using System;
using System.Collections.Generic;

public class Conversation : StoryElement
{
    public bool isComplete { get; private set; }
    float audienceRadius;

    public delegate void ResponseHandler(Conversation convo);
    public event ResponseHandler ParticipantResponded;

    List<Soul> participants;
    internal DialogLine startLine;
    public DialogLine CurrentLine { get; private set; }
    List<DialogLine> possibleResponses;
    //List<StoryPosition> startPositions;

    public Conversation(string id, string[] participantIds) : base(id)
    {
        participants = new List<Soul>(Story.GetElementArray<Soul>(participantIds));

        foreach(Soul soul in participants)
        {
            soul.AddConversation(this);
        }
    }

    public DialogLine Start()
    {
        foreach (Soul soul in participants)
        {
            ParticipantResponded += soul.GetComponent<Soul>().HearResponse;
        }

        Respond(startLine);
        return CurrentLine;
    }

    public void End()
    {
        isComplete = true;
        foreach (Soul soul in participants)
        {
            soul.ExitConversation();
        }
    }

    public void Respond(DialogLine line)
    {
        CurrentLine = line;
        possibleResponses = line.GetResponses();

        if(possibleResponses.Count == 0)
        {
            End();
        }

        //broadcast to participants
        if(ParticipantResponded != null)
        {
            ParticipantResponded(this);
        }
    }

    public bool HasParticipant(Soul soul) {
        return participants.Contains(soul);
    }
}

public class ConversationLoader : DataLoader
{
    public override string fileName { get; protected set; } = "Conversations.json";
    public override Type DataType { get; protected set; } = typeof(Conversation);

    public override object FromJSON(JSONNode data)
    {
        Conversation convo = new Conversation(data["id"], ToStringArray(data["soulIds"].AsArray));
        convo.startLine = Story.GetElementById<DialogLine>(data["startDialogLineId"]);

        AddInstance(convo);
        return convo;
    }
}