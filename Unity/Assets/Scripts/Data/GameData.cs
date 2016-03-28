using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class GameData {

    public string name;
    DateTime lastSaved;
    public Dictionary<string, bool> Conversations { get; private set; }

    public GameData(string aName)
    {
        name = aName;

        Conversations = new Dictionary<string, bool>();

        foreach (var convo in Story.GetElements<Conversation>())
        {
            Conversations.Add(convo.id, false);
        }
    }

    public void Save()
    {
        Conversations.Clear();
        foreach (var convo in Story.GetElements<Conversation>())
        {
            Conversations.Add(convo.id, convo.IsComplete);
        }

        lastSaved = DateTime.Now;
    }

    public DateTime getLastSaved()
    {
        return lastSaved;
    }
}
