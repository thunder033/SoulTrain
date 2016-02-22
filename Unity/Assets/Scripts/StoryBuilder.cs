using System;
using SimpleJSON;
using System.Collections.Generic;

public static class StoryBuilder {

    static private Type[] storyElements = new Type[] { typeof(DialogLine) };

	public static void LoadStoryElements()
    {
        foreach(Type element in storyElements)
        {

        }
    }
}

public abstract class DataLoader<T>
{

    public abstract T GetByID(string id);

    public abstract T FromJSON(JSONNode data);
}

public class DialogLoader : DataLoader<DialogLine>
{
    private Dictionary<string, DialogLine> dialogLines;

    public override DialogLine GetByID(string id)
    {
        return dialogLines[id];
    }

    public override DialogLine FromJSON(JSONNode data)
    {
        return new DialogLine(data);
    }
}