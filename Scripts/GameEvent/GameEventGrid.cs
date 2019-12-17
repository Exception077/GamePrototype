using UnityEngine;
using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameEventGrid : MonoBehaviour {
    public GameEvent Event;
    [SerializeField]
    GameEventManager Manager;
    [SerializeField]
    Image Image;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Event.StartTime.converse2Minutes() == Manager.TC.Minutes) {
            Event.onStart();
            Event.ON = true;
            Manager.GameEvents.Add(Event);
        }
        if (Event.ON) {
            Event.onEffect(new GameTime(Manager.TC.Minutes));
            Manager.showContent(this);
            if (Event.StartTime.converse2Minutes() + Event.Duration == Manager.TC.Minutes) {
                Event.onFinish();
                Manager.GameEvents.Remove(Event);
                Destroy(gameObject);
            }
        }

    }

    public void onSelect() {
        Manager.CurrentGrid = this;
        Manager.showContent(this);
    }

    public void onFlood() {
        Manager.CurrentGrid = null;
    }

    public void hide() {
        Image.enabled = false;
    }

    public void show() {
        try {
            Image.enabled = true;
        }catch(Exception e) {

        }
    }
}
