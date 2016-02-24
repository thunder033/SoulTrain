using SimpleJSON;
using System;
using System.Collections.Generic;

public class Conversation : StoryElement
{
    bool complete;
    float audienceRadius;

    //List<Soul> participants;
    internal DialogLine startLine;
    DialogLine currentLine;
    List<DialogLine> possibleResponses;
    //List<StoryPosition> startPositions;

    public Conversation(string id) : base(id)
    {

    }

    public DialogLine Start()
    {
        currentLine = startLine;
        return currentLine;
    }

    public void Respond(DialogLine line)
    {
        currentLine = line;
        possibleResponses = line.GetResponses();
        //broadcast to participants
    }
}

public class ConversationLoader : DataLoader
{
    public override string fileName { get; protected set; } = "Conversations.json";
    public override Type DataType { get; protected set; } = typeof(Conversation);

    public override object FromJSON(JSONNode data)
    {
        Conversation convo = new Conversation(data["id"]);
        convo.startLine = Story.GetElementById<DialogLine>(data["startDialogLineId"]);

        AddInstance(convo);
        return convo;
    }
}