using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadMenu : MonoBehaviour {

    public Canvas loadMenu;
    public GameObject savePanelPrefab;
    public string titleFieldName;
    public string dateFieldName;

    string selectedGame;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 3; i++)
        {
            GameObject savePanel = Instantiate(savePanelPrefab);
            savePanel.transform.Find(titleFieldName).GetComponent<Text>().text = Game.Saves[i].name;
            savePanel.transform.SetParent(loadMenu.transform);
            savePanel.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(-50 + 50 * i, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadMainMenu()
    {
        Application.LoadLevel("Menu");
    }

    public void LoadGame()
    {

    }
}
