using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Canvas canvas;

    DialogLine line;

    // Use this for initialization
    void Start()
    {
        Vector3 position = new Vector3();
        GameObject trainCar = (GameObject)Instantiate(Resources.Load("Prefabs/TrainCar") as GameObject);
        trainCar.transform.position = position;
        trainCar.transform.Rotate(new Vector3(0, 45, 0));

        Story.LoadStoryElements();

        line = Story.GetElementById<DialogLine>("0");
        string text = line.text;
        GameObject.Find("DialogBox").GetComponent<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            line = line.GetResponses()[0];
            string text = line.text;
            GameObject.Find("DialogBox").GetComponent<Text>().text = text;
        }
    }

    public bool LoadDialogLines()
    {
        //TODO: implement
        return true;
    }
}
