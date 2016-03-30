using System;
using SimpleJSON;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Story {

    static bool loaded = false;
    static public string dataPath {get; } = Application.isEditor ? "Assets/Resources/Data/" : Application.dataPath + "/Resources/";
    static private Dictionary<Type, DataBin> elementLookup = new Dictionary<Type, DataBin>();

    static private DataBin[] dataLoaders = new DataBin[] 
    {
        new SoulLoader(),
        new DialogLoader(),
        new ConversationLoader(),
        new ClueBin(),
        new MysteryBin()
    };

    public static JSONNode Import(DataBin loader)
    {
        loader.OnBeforeLoad();
        elementLookup.Add(loader.DataType, loader);

        var json = JSON.Parse(System.IO.File.ReadAllText(dataPath + loader.fileName));

        for (int i = 0; i < json.Count; i++)
        {
            IStoryElement element = loader.FromJSON(json[i]);
            Debug.Log("Loaded " + loader.DataType + ":" + element.id);
        }

        return json;
    }

    public static void LoadGame(GameData game)
    {
        foreach (DataBin loader in dataLoaders)
        {
            loader.LoadGame(game);
        }
    }

	public static void LoadStoryElements()
    {
        if(!loaded)
        {
            foreach (DataBin loader in dataLoaders)
            {
                Import(loader);
            }
            loaded = true;
        }
    }

    public static T GetElementById<T>(string id) where T : IStoryElement
    {
        return (T)elementLookup[typeof(T)].GetByID(id);
    }

    public static T[] GetElements<T>() where T : IStoryElement
    {
        return elementLookup[typeof(T)].GetAll().Cast<T>().ToArray();
    }

    public static T[] GetElements<T>(string[] ids) where T : IStoryElement
    {
        T[] elements = new T[ids.Length];

        for (int i = 0; i < ids.Length; i++)
        {
            elements[i] = GetElementById<T>(ids[i]);
        }

        return elements;
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