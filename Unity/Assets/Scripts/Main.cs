using UnityEngine;
using System;
using SimpleJSON;
using System.Collections.Generic;
using System.Collections;

public class Main : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 position = new Vector3();
        GameObject trainCar = (GameObject)Instantiate(Resources.Load("Prefabs/TrainCar") as GameObject);
        trainCar.transform.position = position;
        trainCar.transform.Rotate(new Vector3(0, 45, 0));

        string dataPath = "Assets/Resources/Data/";
        List<Tuple<string, Type>> scriptedEntities = new List<Tuple<string, Type>>();
        //scriptedEntities.Add("DialogLines.json", typeof(DialogLine));

        

        var dialogLinesJSON = JSON.Parse(System.IO.File.ReadAllText());

        for (int i = 0; i < dialogLinesJSON.Count; i++)
        {
            var line = new DialogLine(dialogLinesJSON[i]);
            DialogLine.byID.Add(line.id, line);
            Debug.Log(line.text);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool LoadDialogLines()
    {
        //TODO: implement
        return true;
    }
}
