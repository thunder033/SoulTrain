using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

[RequireComponent(typeof(Selectable))]
public class SaveOption : MonoBehaviour, ISelectHandler, IDeselectHandler {

    GameData _save;
    Selectable select;

    public string titleFieldName;
    public string dateFieldName;
    public string saveActionName;
    public string deleteActionName;

    float selectTimeOut = .1f;
    float selectTimer = 0;
    bool selected = false;


    // Use this for initialization
    void Start () {
        select = GetComponent<Selectable>();

        if(_save == null)
        {
            transform.Find(titleFieldName).GetComponent<Text>().text = "New Game";
            transform.Find(dateFieldName).GetComponent<Text>().text = "";
        }

        transform.Find(saveActionName).gameObject.SetActive(false);
        transform.Find(saveActionName).GetComponent<Button>().onClick.AddListener(() => LoadGame());

        transform.Find(deleteActionName).gameObject.SetActive(false);
        transform.Find(deleteActionName).GetComponent<Button>().onClick.AddListener(() => DeleteGame());

    }
	
	// Update is called once per frame
	void Update () {
	    if(!selected && selectTimer > 0)
        {
            selectTimer -= Time.deltaTime;
        }
        else if(!selected)
        {
            transform.Find(saveActionName).gameObject.SetActive(false);
            transform.Find(deleteActionName).gameObject.SetActive(false);
            GetComponent<Selectable>().interactable = true;
        }
	}

    public void SetGameData(GameData save)
    {
        _save = save;
        transform.Find(titleFieldName).GetComponent<Text>().text = save.name;
        transform.Find(dateFieldName).GetComponent<Text>().text = string.Format("{0:M/d/yyyy HH:mm tt}", save.getLastSaved());
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameObject saveAction = transform.Find(saveActionName).gameObject;
        transform.Find(deleteActionName).gameObject.SetActive(_save != null);
        saveAction.SetActive(true);
        saveAction.GetComponentInChildren<Text>().text = _save == null ? "Start Game" : "Continue";
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
            Game.NewGame("newGame-" + System.DateTime.UtcNow.ToString());
            Application.LoadLevel("Train");
        }
    }

    public void DeleteGame()
    {
        if(_save != null)
        {
            Game.DeleteSave(_save);
            Game.Save();

            transform.Find(titleFieldName).GetComponent<Text>().text = "New Game";
            transform.Find(dateFieldName).GetComponent<Text>().text = "";
            _save = null;
        }
    }
}
