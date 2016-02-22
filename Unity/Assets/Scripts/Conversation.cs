using SimpleJSON;
using System.Collections.Generic;

public class Conversation {

    public static Dictionary<string, Conversation> byID = new Dictionary<string, Conversation>();

    public List<DialogLine> responses = new List<DialogLine>();

    public string id;
    bool complete;
    float audienceRadius;

    //List<Soul> participants;
    DialogLine startLine;
    DialogLine currentLine;
    List<DialogLine> possibleResponses;
    //List<StoryPosition> startPositions;

    public Conversation(JSONNode data)
    {
        id = data["id"];
        startLine = DialogLine.byID[data["startDialogLineId"]];
    }

    DialogLine Start()
    {
        currentLine = startLine;
    }
}
