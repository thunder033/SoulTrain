using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 position = new Vector3();
        GameObject trainCar = (GameObject)Instantiate(Resources.Load("/Prefabs/TrainCar"));
        trainCar.transform.position = position;
        trainCar.transform.Rotate(new Vector3(0, 0, 90));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
