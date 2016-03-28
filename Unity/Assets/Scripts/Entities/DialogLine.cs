using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;


public class DialogLine : StoryElement
{
    public Soul speaker;
    private string[] responseIDs;
    private string[] clueIDs;
    public string text;

    public bool IsLastLine
    {
        get { return responseIDs.Length == 0; }
    }

    public bool IsMonologue
    {
        get
        {
            if (responseIDs.Length == 1)
            {
                return Story.GetElementById<DialogLine>(responseIDs[0]).speaker == speaker;
            }

            return false;
        }
    }

    public DialogLine(string id, string aText, string soulId) : base(id)
    {
        speaker = Story.GetElementById<Soul>(soulId);
        text = aText;
    }

    public void SetReponseIDs(string[] responseIDs)
    {
        this.responseIDs = responseIDs;
    }

    public void SetClueIDs(string[] clueIDs)
    {
        this.clueIDs = clueIDs;
    }

    public List<DialogLine> GetResponses()
    {
        return new List<DialogLine>(Story.GetElements<DialogLine>(responseIDs));
    }

    public List<Clue> GetClues()
    {
        return new List<Clue>(Story.GetElements<Clue>(clueIDs));
    }

    public List<DialogLine> GetResponses(Soul soul)
    {
        List<DialogLine> responses = GetResponses();
        List<DialogLine> soulResponses = new List<DialogLine>();

        foreach (DialogLine line in responses)
        {
            if(line.speaker == soul) {
                soulResponses.Add(line);
            }
        }

        return soulResponses;
    }
}

public class DialogLoader : DataBin
{
    public override string fileName { get; protected set; } = "DialogLines.json";
    public override Type DataType { get; protected set; } = typeof(DialogLine);

    public override IStoryElement FromJSON(JSONNode data)
    {
        DialogLine line = new DialogLine(data["id"], data["text"], data["soulId"]);
        line.SetReponseIDs(ToStringArray(data["responseIds"].AsArray));
        line.SetClueIDs(ToStringArray(data["clueIds"].AsArray));
        AddInstance(line);
        return line;
    }
}