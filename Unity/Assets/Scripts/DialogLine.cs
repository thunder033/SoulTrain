using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

public class DialogLine {

    public static Dictionary<string, DialogLine> byID = new Dictionary<string, DialogLine>();

    public List<DialogLine> responses = new List<DialogLine>();

    public string id;
    public string text;

    public DialogLine(JSONNode data)
    {
        id = data["id"];
        text = data["text"];
    }
}