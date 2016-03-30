using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public static Dictionary<string, GameData> Saves { get; private set; } = new Dictionary<string, GameData>();
    static GameData current;
    static string loadOnStart;

    public static string saveName = "/savedGames.gd";

    public void Awake()
    {
        if(Instance = null)
        {
            Instance = this;
        }

        else if (Instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a Game.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        Load();

        if (loadOnStart != null)
        {
            try
            {
                LoadSave(loadOnStart);
            }
            catch(KeyNotFoundException)
            {
                LoadSave(new GameData(loadOnStart));
            }
            
        }
    }

    public static bool SaveExists(string name)
    {
        return Saves.ContainsKey(name);
    }

    public static GameData LoadSave(string name)
    {
        return LoadSave(Saves[name]);
    }

    public static GameData LoadSave(GameData game)
    {
        current = game;
        Story.LoadGame(current);
        return current;
    }

    public static void Load()
    {
        if(File.Exists(Application.persistentDataPath + saveName))
        {

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + saveName, FileMode.Open);
                List<GameData> SavesList = (List<GameData>)bf.Deserialize(file);
                file.Close();

                foreach(GameData save in SavesList)
                {
                    if (!SaveExists(save.name))
                        Saves.Add(save.name, save);
                }
                Debug.Log("Loaded " + Saves.Count + " Saves from " + Application.persistentDataPath);
            }
            catch(SerializationException ex)
            {
                Debug.LogError(ex);
                //TODO: Handle serializaton exception
            }
        }

        if (Saves.Count > 0)
        {
            //LoadSave(Saves[0]);
        }
        else
        {
            LoadSave(new GameData("__default"));
        }
    }

    public static void Save()
    {
        if(current != null)
        {
            current.Save();
            if (!SaveExists(current.name))
            {
                Saves.Add(current.name, current);
            }
        }
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveName);
        List<GameData> savesList = new List<GameData>(Saves.Values);
        bf.Serialize(file, savesList);
        file.Close();
    }

    public static void DeleteSave(GameData game)
    {
        Saves.Remove(game.name);
    }

    public static void LoadOnStart(GameData game)
    {
        loadOnStart = game.name;
    }

    public static void NewGame(string name)
    {
        loadOnStart = name;
    }
}

public interface ILoadable
{
    void LoadGame(GameData game);
}