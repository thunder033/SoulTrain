using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class MysteriesPage : MonoBehaviour, IPage {

    public MysteryBlock mysteryBlockPrefab;
    Journal journal;

    public void OnLoad(Journal journal) {
        this.journal = journal;
        //Remove all existing mystery blocks that are displayed
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        //Get all mysteries where the first clue has been discovered
        Mystery[] mysteries = Story.GetElements<Mystery>().Where(m => m.FirstClue.Discovered).ToArray();
        //Mystery[] mysteries = Story.GetElements<Mystery>();

        //Create new blocks for them
        for (int i = 0; i < mysteries.Length; i++) {
            MysteryBlock mysteryBlock = Instantiate(mysteryBlockPrefab);
            mysteryBlock.SetMystery(mysteries[i]);
            mysteryBlock.transform.SetParent(transform);
            mysteryBlock.GetComponent<Button>().onClick.AddListener(() => ViewMystery(mysteryBlock.GetMystery()));
            mysteryBlock.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0, 70 - 70 * i);
        }
    }

    public void ViewMystery(Mystery mystery) {
        journal.OpenSection(Journal.Section.MysteryPage);
    }
}
