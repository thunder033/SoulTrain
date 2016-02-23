﻿using System;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine;

public static class Story {

    static public string dataPath {get; } = "Assets/Resources/Data/";
    static private Dictionary<Type, DataLoader> elementLookup;

    static private DataLoader[] dataLoaders = new DataLoader[] 
    {
        new DialogLoader(),
        new ConversationLoader()
    };

	public static void LoadStoryElements()
    {
        elementLookup = new Dictionary<Type, DataLoader>();
        foreach(DataLoader loader in dataLoaders)
        {
            elementLookup.Add(loader.DataType, loader);

            var json = JSON.Parse(System.IO.File.ReadAllText(dataPath + loader.fileName));

            for (int i = 0; i < json.Count; i++)
            {
                StoryElement element = (StoryElement)loader.FromJSON(json[i]);
                Debug.Log("Loaded " + loader.DataType + ":" + element.id);
            }
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