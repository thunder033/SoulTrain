using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SimpleJSON;

public class Clue : StoryElement {

    public string Name { get; private set; }
    public string Text { get; private set; }
    public string Hint { get; private set; }

    public bool Discovered { get; private set; }

    private Clue _previousClue;

    public Clue PreviousClue
    {
        get { return _previousClue; }
        set {
            if(_previousClue == null && value != null)
            {
                _previousClue = value;
                value.NextClue = this;
            }
        }
    }

    private Clue _nextClue;

    public Clue NextClue
    {
        get { return _nextClue; }
        set {
            if(_nextClue == null && value != null)
            {
                _nextClue = value;
                value.PreviousClue = this;
            }
            
        }
    }

    public string[] soulIDs { private get; set; }

    public List<Soul> SoulsOfInterest
    {
        get { return new List<Soul>(Story.GetElements<Soul>(soulIDs)); }
    }

    public Clue(string id, string name, string text, string hint) : base(id)
    {
        Name = name;
        Text = text;
        Hint = hint;
    }

    public void Reveal()
    {
        Discovered = true;
        Debug.Log("Discovered " + Name);
        Notification notification = ((GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Notification"))).GetComponent<Notification>();
        notification.Show("Discovered Clue: " + Name);
    }
}

public class ClueBin : DataBin
{
    public override string fileName { get; protected set; } = "Clues.json";

    public override Type DataType { get; protected set; } = typeof(Clue);

    public override IStoryElement FromJSON(JSONNode data)
    {
        Clue clue = new Clue(data["id"], data["name"], data["description"], data["hint"]);
        clue.NextClue = Story.GetElementById<Clue>(data["nextClueId"]);
        clue.PreviousClue = Story.GetElementById<Clue>(data["prevClueId"]);
        clue.soulIDs = ToStringArray(data["soulsOfInterestIds"].AsArray);

        AddInstance(clue);
        return clue;
    }
}