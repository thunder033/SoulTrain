using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Selectable))]
public class SaveOption : MonoBehaviour, ISelectHandler {

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

    bool callout = false;
    float calloutTimeOut = .75f;
    float calloutTimer = 0;

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
        nameInput.onEndEdit.AddListener(e => titleField.text = nameInput.text);
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
            Deactivate();
        }

        if (calloutTimer > 0)
        {
            calloutTimer -= Time.deltaTime;
            callout = true;
        }
        else if(callout)
        {
            setInputColor(Color.white);
            callout = false;
        }
        
	}

    void setInputColor(Color color)
    {
        //Call the copy constructor on the color block
        ColorBlock inputColors = nameInput.colors;
        inputColors.normalColor = color;
        nameInput.colors = inputColors;
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

        var saveOptions = new List<Selectable>(Selectable.allSelectables.Where(s => s.GetComponent<SaveOption>() != null));
        for (int i = 0; i < saveOptions.Count(); i++)
        {
            if(saveOptions[i] != GetComponent<Selectable>())
            {
                saveOptions[i].gameObject.GetComponent<SaveOption>().Deactivate();
            }
        }
    }

    public void Deactivate()
    {
        loadControl.gameObject.SetActive(false);
        deleteControl.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
        titleField.gameObject.SetActive(true);
    }

    public void LoadGame()
    {
        if(_save != null)
        {
            Game.LoadSave(_save.name);
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
                setInputColor(Color.red);
                calloutTimer = calloutTimeOut;
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

            Deactivate();
        }
    }
}
