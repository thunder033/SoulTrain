using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject game;

    // Use this for initialization
    void Start()
    {
        if(Game.Instance == null)
        {
            Instantiate(game);
        }
    }
}
