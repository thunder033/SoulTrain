using UnityEngine;
using System.Collections;


public class TrainLoader : SceneLoader
{
    Journal journal;
    public void Awake()
    {
        Story.LoadStoryElements();
    }

    public new void Start()
    {
        base.Start();
        journal = GameObject.Find("JournalGUI").GetComponent<Journal>();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            journal.Open();
    }

    public void ExitToMenu()
    {
        Application.LoadLevel("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}