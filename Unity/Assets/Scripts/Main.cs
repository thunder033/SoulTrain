using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Canvas canvas;

    // Use this for initialization
    void Start()
    {
        Vector3 position = new Vector3();
        GameObject trainCar = Instantiate(Resources.Load("Prefabs/TrainCar") as GameObject);
        trainCar.transform.position = position;
        trainCar.transform.Rotate(new Vector3(0, 45, 0));

        Story.LoadStoryElements();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
