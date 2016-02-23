using UnityEngine;
using SimpleJSON;
using System;
using System.Collections.Generic;


public class DialogLine : StoryElement
{
    internal List<DialogLine> responses = new List<DialogLine>();
    public string text;

    public DialogLine(string id) : base(id)
    {
    }

    public List<DialogLine> GetResponses()
    {
        return responses;
    }
}

public class DialogLoader : DataLoader
{
    public override string fileName { get; protected set; } = "DialogLines.json";
    public override Type DataType { get; protected set; } = typeof(DialogLine);

    public override object FromJSON(JSONNode data)
    {
        DialogLine line = new DialogLine(data["id"]);
        line.text = data["text"];

        for (int i = 0; i < data["responseIds"].AsInt; i++)
        {
            line.responses.Add(GetByID(data["responseIds"][i]) as DialogLine);
        }

        AddInstance(line);
        return line;
    }
}