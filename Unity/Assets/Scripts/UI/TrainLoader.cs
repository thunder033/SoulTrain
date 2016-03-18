using UnityEngine;
using System.Collections;


public class TrainLoader : SceneLoader
{
    public void Awake()
    {
        Story.LoadStoryElements();
    }
}