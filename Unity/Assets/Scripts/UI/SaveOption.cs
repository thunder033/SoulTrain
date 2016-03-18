using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

[RequireComponent(typeof(Selectable))]
public class SaveOption : MonoBehaviour, ISelectHandler, IDeselectHandler {

    GameData _save;
    Selectable select;

    public Text titleField;
    public Text dateField;
    public Button loadControl;
    public Button deleteControl;
    public InputField nameInput;

    float selectTimeOut = .1f;
    float selectTimer = 0;
    bool selected = false;


    // Use this for initialization
    void Start () {
        select = GetComponent<Selectable>();

        if (_save == null)
        {
            titleField.text = "New Game";
            dateField.text = "";
        }

        dateField.gameObject.SetActive(_save != null);
        loadControl.onClick.AddListener(() => LoadGame());
        deleteControl.gameObject.SetActive(false);
        deleteControl.onClick.AddListener(() => DeleteGame());

    }
	
	// Update is called once per frame
	void Update () {
	    if(!selected && selectTimer > 0)
        {
            selectTimer -= Time.deltaTime;
        }
        else if(!selected)
        {
            loadControl.gameObject.SetActive(false);
            deleteControl.gameObject.SetActive(false);
            GetComponent<Selectable>().interactable = true;
        }
	}

    public void SetGameData(GameData save)
    {
        _save = save;
        titleField.text = save.name;

        dateField.gameObject.SetActive(true);
        dateField.text = string.Format("{0:M/d/yyyy HH:mm tt}", save.getLastSaved());
    }

    public void OnSelect(BaseEventData eventData)
    {
        deleteControl.gameObject.SetActive(_save != null);
        nameInput.gameObject.SetActive(_save == null);
        loadControl.gameObject.SetActive(true);
        loadControl.gameObject.GetComponentInChildren<Text>().text = _save == null ? "Start Game" : "Continue";
        selected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selected = false;
        selectTimer = selectTimeOut;
    }

    public void LoadGame()
    {
        if(_save != null)
        {
            Game.LoadSave(_save);
            Application.LoadLevel("Train");
        }
        else
        {
            string name = nameInput.text;

            if(name.Length > 0 && !Game.SaveExists(name))
            {
                Game.NewGame(name);
                Application.LoadLevel("Train");
            }
            else
            {
                //nameInput.GetComponent<InputField>().colors.normalColor = Color.red;
            }
        }
    }

    public void DeleteGame()
    {
        if(_save != null)
        {
            Game.DeleteSave(_save);
            Game.Save();

            titleField.text = "New Game";
            dateField.text = "";
            _save = null;
        }
    }
}
