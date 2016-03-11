using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject game;

    // Use this for initialization
    public void Start()
    {
        if(Game.Instance == null)
        {
            Instantiate(game);
        }
    }

    public void NewGame()
    {
        Game.NewGame("newGame-" + System.DateTime.UtcNow.ToString());
        Application.LoadLevel("Train");
    }

    public void ContinueGame()
    {
        Application.LoadLevel("LoadGame");
    }
}