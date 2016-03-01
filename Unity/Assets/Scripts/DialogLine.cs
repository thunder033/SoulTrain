using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;


public class DialogLine : StoryElement
{
    public Soul speaker;
    private string[] responseIDs;
    public string text;

    public DialogLine(string id, string aText, string soulId) : base(id)
    {
        speaker = Story.GetElementById<Soul>(soulId);
        text = aText;
    }

    public void SetReponseIDs(string[] aResponseIds)
    {
        responseIDs = aResponseIds;
    }

    public List<DialogLine> GetResponses()
    {
        return new List<DialogLine>(Story.GetElementArray<DialogLine>(responseIDs));
    }
}

public class DialogLoader : DataLoader
{
    public override string fileName { get; protected set; } = "DialogLines.json";
    public override Type DataType { get; protected set; } = typeof(DialogLine);

    public override object FromJSON(JSONNode data)
    {
        DialogLine line = new DialogLine(data["id"], data["text"], data["soulId"]);
        line.SetReponseIDs(ToStringArray(data["responseIds"].AsArray));
        AddInstance(line);
        return line;
    }
}