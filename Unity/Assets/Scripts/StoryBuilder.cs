using System;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public static class Story {

    static public string dataPath {get; } = "Assets/Resources/Data/";
    static private Dictionary<Type, DataLoader> elementLookup = new Dictionary<Type, DataLoader>();

    static private DataLoader[] dataLoaders = new DataLoader[] 
    {
        new SoulLoader(),
        new DialogLoader(),
        new ConversationLoader()
    };

    public static JSONNode Import(DataLoader loader)
    {
        elementLookup.Add(loader.DataType, loader);

        var json = JSON.Parse(System.IO.File.ReadAllText(dataPath + loader.fileName));

        for (int i = 0; i < json.Count; i++)
        {
            IStoryElement element = (IStoryElement)loader.FromJSON(json[i]);
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

    public static T GetElementById<T>(string id) where T : IStoryElement
    {
        return (T)elementLookup[typeof(T)].GetByID(id);
    }

    public static T[] GetElementArray<T>(string[] ids) where T : IStoryElement
    {
        T[] elements = new T[ids.Length];

        for (int i = 0; i < ids.Length; i++)
        {
            elements[i] = GetElementById<T>(ids[i]);
        }

        return elements;
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

    protected void AddInstance(IStoryElement element)
    {
        elementMap.Add(element.id, element);
    }

    public abstract object FromJSON(JSONNode data);

    internal static string[] ToStringArray(JSONArray aNode)
    {
        string[] stringArray = new string[aNode.Count];

        for (int i = 0; i < stringArray.Length; i++)
        {
            stringArray[i] = aNode[i];
        }

        return stringArray;
    }
}

public interface IStoryElement
{
    string id { get; }
}

public abstract class StoryElement : IStoryElement
{
    public string id { get; private set; }

    public StoryElement(string a_id)
    {
        id = a_id;
    }
}