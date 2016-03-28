using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class Mystery : StoryElement {


    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Resolution { get; private set; }
    public Clue FirstClue { get; private set; }
    public Soul Soul { get; private set; }

    private Mystery(string id) : base(id)
    {

    }

    public sealed class Builder
    {
        private Mystery mystery;

        public Builder(string mysteryId)
        {
            mystery = new Mystery(mysteryId);
        }

        public string Name
        {
           set { mystery.Name = value; }
        }

        public string Description
        {
            set { mystery.Description = value; }
        }

        public string Resolution
        {
            set { mystery.Resolution = value; }
        }

        public Clue FirstClue
        {
            set { mystery.FirstClue = value; }
        }

        public Soul Soul
        {
            set { mystery.Soul = value; }
        }

        public Mystery Build()
        {
            Mystery ret = mystery;
            mystery = null;
            return ret;
        }

        public static implicit operator Mystery(Builder builer)
        {
            return builer.Build();
        }
    }
}

public class MysteryBin : DataBin
{
    public override string fileName { get; protected set; } = "Mystries.json";
    public override Type DataType { get; protected set; } = typeof(Mystery);

    public override IStoryElement FromJSON(JSONNode data)
    {
        Mystery mystery = new Mystery.Builder(data["id"])
        {
            Name = data["name"],
            Description = data["description"],
            Resolution = data["resolution"],
            Soul = Story.GetElementById<Soul>(data["soulId"]),
            FirstClue = Story.GetElementById<Clue>(data["firstClueId"])
        };

        return mystery;
    }
}