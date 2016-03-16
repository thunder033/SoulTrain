using UnityEngine;
using UnityEngine.UI;

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

            if (i < Game.Saves.Count)
            {
                GameData save = Game.Saves[i];
                savePanel.GetComponent<SaveOption>().SetGameData(save);
                savePanel.transform.Find(titleFieldName).GetComponent<Text>().text = save.name;
                savePanel.transform.Find(dateFieldName).GetComponent<Text>().text = string.Format("{0:M/d/yyyy HH:mm tt}", save.getLastSaved());
            }

            savePanel.transform.SetParent(loadMenu.transform);
            savePanel.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0, 70 - 70 * i);
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
