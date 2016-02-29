using System;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public static class Story {

    static public string dataPath {get; } = "Assets/Resources/Data/";
    static private Dictionary<Type, DataLoader> elementLookup = new Dictionary<Type, DataLoader>();

    static private DataLoader[] dataLoaders = new DataLoader[] 
    {
        new DialogLoader(),
        new ConversationLoader()
    };

    public static JSONNode Import(DataLoader loader)
    {
        elementLookup.Add(loader.DataType, loader);

        var json = JSON.Parse(System.IO.File.ReadAllText(dataPath + loader.fileName));

        for (int i = 0; i < json.Count; i++)
        {
            StoryElement element = (StoryElement)loader.FromJSON(json[i]);
            Debug.Log("Loaded " + loader.DataType + ":" + element.id);
        }

        return json;
    }

	public static void LoadStoryElements()
    {
        foreach(DataLoader loader in dataLoaders)
        {
            Import(loader);
        }
    }

    public static T GetElementById<T>(string id)
    {
        return (T)elementLookup[typeof(T)].GetByID(id);
    }
}

public abstract class DataLoader
{
    public abstract string fileName { get; protected set; }

    public abstract Type DataType { get; protected set; }

    public virtual void PostLoad()
    {

    }

    private Dictionary<string, object> elementMap;

    public DataLoader()
    {
        elementMap = new Dictionary<string, object>();
    }

    public object GetByID(string id) {
        return elementMap[id];
    }

    protected void AddInstance(StoryElement element)
    {
        elementMap.Add(element.id, element);
    }

    public abstract object FromJSON(JSONNode data);
}

public abstract class StoryElement
{
    public string id { get; private set; }

    public StoryElement(string a_id)
    {
        id = a_id;
    }
}