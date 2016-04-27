using UnityEngine;
using System.Collections.Generic;
using System;

public class Journal : MonoBehaviour {

    enum Section {
        Mysteries,
        PauseMenu
    }

    Dictionary<Section, GameObject> sections;
    // Use this for initialization
    void Start() {
        //index the journal sections
        sections = new Dictionary<Section, GameObject>();
        sections.Add(Section.Mysteries, GameObject.Find(Section.Mysteries.ToString()));
        sections.Add(Section.PauseMenu, GameObject.Find(Section.PauseMenu.ToString()));

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

    void OpenSection(Section name) {
        foreach (GameObject section in sections.Values) {
            section.SetActive(false);
        }

        sections[name].SetActive(true);
        IPage page = sections[name].GetComponent<IPage>();
        if (page != null) {
            page.OnLoad();
        }
    }
}

public interface IPage {
    void OnLoad();
}
