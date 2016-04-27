using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenu : MonoBehaviour {

    public Canvas loadMenu;
    public GameObject savePanelPrefab;

    string selectedGame;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < 3; i++) {

            GameObject savePanel = Instantiate(savePanelPrefab);

            List<GameData> savesList = new List<GameData>(Game.Saves.Values);
            if (i < savesList.Count) {
                GameData save = savesList[i];
                savePanel.GetComponent<SaveOption>().SetGameData(save);
            }

            savePanel.transform.SetParent(loadMenu.transform);
            savePanel.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0, 70 - 70 * i);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void LoadMainMenu() {
        Application.LoadLevel("Menu");
    }

    public void LoadGame() {

    }
}
