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


    // Use this for initialization
    void Start () {
        select = GetComponent<Selectable>();
        transform.Find(titleFieldName).GetComponent<Text>().text = "New Game";
        transform.Find(dateFieldName).GetComponent<Text>().text = "";
        transform.Find(saveActionName).gameObject.SetActive(false);
        transform.Find(deleteActionName).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetGameData(GameData save)
    {
        _save = save;
        Debug.Log(transform.gameObject);
        transform.gameObject.transform.Find(titleFieldName).GetComponent<Text>().text = save.name;
        transform.gameObject.transform.Find(dateFieldName).GetComponent<Text>().text = string.Format("{0:M/d/yyyy HH:mm tt}", save.getLastSaved());
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameObject saveAction = transform.Find(saveActionName).gameObject;
        transform.Find(deleteActionName).gameObject.SetActive(_save != null);
        saveAction.SetActive(true);
        saveAction.GetComponentInChildren<Text>().text = _save == null ? "Start Game" : "Continue";
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.Find(saveActionName).gameObject.SetActive(false);
        transform.Find(deleteActionName).gameObject.SetActive(false);
    }

    public void LoadGame()
    {

    }

    public void DeleteGame()
    {

    }
}
