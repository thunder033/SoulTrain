using UnityEngine;
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

        var dialogLinesJSON = JSON.Parse(System.IO.File.ReadAllText("Assets/Resources/Data/DialogLines.json"));

        for (int i = 0; i < dialogLinesJSON.Count; i++)
        {
            var line = new DialogLine(dialogLinesJSON[i]);
            DialogLine.byID.Add(line.id, line);
            Debug.Log(line.text);
        }

        var conversationsJSON = JSON.Parse(System.IO.File.ReadAllText("Assets/Resources/Data/Conversations.json"));

        for (int i = 0; i < conversationsJSON.Count; i++)
        {
            var line = new Conversation(conversationsJSON[i]);
            Conversation.byID.Add(line.id, line);
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
