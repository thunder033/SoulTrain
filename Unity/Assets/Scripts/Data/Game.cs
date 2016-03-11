using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public static List<GameData> Saves { get; private set; } = new List<GameData>();
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

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
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
            catch(System.InvalidOperationException)
            {
                LoadSave(new GameData(loadOnStart));
            }
            
        }
    }

    public static GameData LoadSave(string name)
    {
        return LoadSave(Saves.Where(save => save.name == name).First());
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
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveName, FileMode.Open);
            Saves = (List<GameData>)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded " + Saves.Count + " Saves from " + Application.persistentDataPath);
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
        current.Save();
        if(!Saves.Contains(current))
        {
            Saves.Add(current);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + saveName);
        bf.Serialize(file, Saves);
        file.Close();
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