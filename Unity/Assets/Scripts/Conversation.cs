using SimpleJSON;
using System;
using System.Collections.Generic;

public class Conversation : StoryElement
{
    public bool IsComplete { get; private set; }
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
        participants = new List<Soul>(Story.GetElements<Soul>(participantIds));

        foreach(Soul soul in participants)
        {
            soul.AddConversation(this);
        }
    }

    public void Load(GameData game)
    {
        SetComplete(game.Conversations[id]);
    }

    private void SetComplete(bool isComplete)
    {
        IsComplete = isComplete;
    }

    public DialogLine Start()
    {
        foreach (Soul soul in participants)
        {
            soul.JoinConversation(this);
            ParticipantResponded += soul.GetComponent<Soul>().HearResponse;
        }

        Respond(startLine);
        return CurrentLine;
    }

    public void Continue()
    {
        if (CurrentLine.IsMonologue)
        {
            Respond(CurrentLine.GetResponses()[0]);
        }
    }

    public void End()
    {
        SetComplete(true);
        foreach (Soul soul in participants)
        {
            soul.ExitConversation(false);
        }
        Game.Save();
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

public class ConversationLoader : DataBin
{
    public override string fileName { get; protected set; } = "Conversations.json";
    public override Type DataType { get; protected set; } = typeof(Conversation);

    public override IStoryElement FromJSON(JSONNode data)
    {
        Conversation convo = new Conversation(data["id"], ToStringArray(data["soulIds"].AsArray));
        convo.startLine = Story.GetElementById<DialogLine>(data["startDialogLineId"]);

        AddInstance(convo);
        return convo;
    }

    public override void LoadGame(GameData game)
    {
        foreach (Conversation convo in GetAll())
        {
            convo.Load(game);
        }
    }
}