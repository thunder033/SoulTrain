using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Notification : MonoBehaviour {

    Text textComp;
    public float displayTime;
    float displayedElapsed;

    static List<Notification> activeNotifications;
    int notificationMargin = 10;

	// Use this for initialization
	void Start () {
        if(activeNotifications == null) {
            activeNotifications = new List<Notification>();
        }

        int offset = getYOffset(activeNotifications.Select(a => a.GetComponent<RectTransform>()).ToList(), notificationMargin);
        Debug.Log(offset);
        GetComponent<RectTransform>().anchoredPosition = new Vector2(120, -70 - offset);

        activeNotifications.Add(this);

        displayedElapsed = 0;
	}

    void MoveNotificationsUp(int offset) {
        foreach(Notification notification in activeNotifications) {
            RectTransform rect = notification.GetComponent<RectTransform>();
            Vector2 pos = rect.anchoredPosition;
            rect.anchoredPosition = new Vector2(pos.x, pos.y + offset);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (enabled) {
            displayedElapsed += Time.deltaTime;
            if (displayedElapsed >= displayTime) {
                activeNotifications.Remove(this);
                Destroy(gameObject);
                MoveNotificationsUp((int)GetComponent<RectTransform>().rect.height + notificationMargin);
            }
        }
	}

    int getYOffset(List<RectTransform> elements, int margin = 0) {
        if (elements == null)
            return 0;

        return elements.Aggregate(0, (int a, RectTransform b) => {
            return a + (int)b.rect.height + margin;
        });
    }

    public void Show(string text) {
        if (textComp == null) {
            textComp = GetComponentInChildren<Text>();
        }

        textComp.text = text;
        if (transform.parent == null) {
            transform.SetParent(GameObject.Find("HUD").transform);
        }

        enabled = true;
    }
}
