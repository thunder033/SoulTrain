using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Notification : MonoBehaviour {

    Text textComp;
    public float displayTime;
    float displayedElapsed;

	// Use this for initialization
	void Start () {
        
        //enabled = false;
        displayedElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (enabled) {
            displayedElapsed += Time.deltaTime;
            if (displayedElapsed >= displayTime) {
                Destroy(gameObject);
            }
        }
	}

    public void Show(string text) {
        if (textComp == null) {
            textComp = GetComponentInChildren<Text>();
        }

        textComp.text = text;
        if (transform.parent == null) {
            transform.SetParent(GameObject.Find("HUD").transform);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(120, -70);
        }

        enabled = true;
    }
}
