using UnityEngine;
using System;
using SimpleJSON;
using System.Collections.Generic;


public abstract class DataBin
{
    public abstract string fileName { get; protected set; }

    public abstract Type DataType { get; protected set; }


    public virtual void LoadGame(GameData game)
    {

    }

    public virtual void OnBeforeLoad()
    {

    }

    private Dictionary<string, IStoryElement> elementMap;

    public DataBin()
    {
        elementMap = new Dictionary<string, IStoryElement>();
    }

    public virtual List<IStoryElement> GetAll()
    {
        return new List<IStoryElement>(elementMap.Values);
    }

    public object GetByID(string id)
    {
        try
        {
            return elementMap[id];
        }
        catch(Exception)
        {
            return null;
        }
        
    }

    protected void AddInstance(IStoryElement element)
    {
        elementMap.Add(element.id, element);
    }

    public abstract IStoryElement FromJSON(JSONNode data);

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
