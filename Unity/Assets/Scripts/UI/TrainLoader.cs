using UnityEngine;
using System.Collections;


public class TrainLoader : Loader
{
    public void Awake()
    {
        Story.LoadStoryElements();
    }
}