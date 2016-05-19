using UnityEngine;
using System.Collections.Generic;
using System;

public class Journal : MonoBehaviour {

    public enum Section {
        Mysteries,
        PauseMenu,
        MysteryPage
    }

    Dictionary<Section, GameObject> sections;
    // Use this for initialization
    void Start() {
        //index the journal sections
        sections = new Dictionary<Section, GameObject>();
        sections.Add(Section.Mysteries, GameObject.Find(Section.Mysteries.ToString()));
        sections.Add(Section.PauseMenu, GameObject.Find(Section.PauseMenu.ToString()));
        sections.Add(Section.MysteryPage, GameObject.Find(Section.MysteryPage.ToString()));

        //Hide the journal
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            OpenSection(Section.Mysteries);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            OpenSection(Section.PauseMenu);
    }

    public void Open() {
        gameObject.SetActive(true);
        OpenSection(Section.PauseMenu);
    }

    public void Close() {
        gameObject.SetActive(false);
    }

    public GameObject GetSection(Section name) {
        return sections[name];
    }

    public void OpenSection(Section name) {
        foreach (GameObject section in sections.Values) {
            section.SetActive(false);
        }

        sections[name].SetActive(true);
        IPage page = sections[name].GetComponent<IPage>();
        if (page != null) {
            page.OnLoad(this);
        }
    }

    public void openPause()
    {
        OpenSection(Section.PauseMenu);
    }
    public void openMysteries()
    {
        OpenSection(Section.Mysteries);
    }
}



public interface IPage {
    void OnLoad(Journal journal);
}
