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

        Soul soul0 = GameObject.Find("Soul0").GetComponent<Soul>();
        Soul soul1 = GameObject.Find("Soul1").GetComponent<Soul>();
        soul0.Converse(soul1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
