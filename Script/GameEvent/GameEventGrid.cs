using UnityEngine;
using System.Collections;
using TMPro;

public class GameEventGrid : MonoBehaviour {
    public GameEvent Event;
    [SerializeField]
    GameEventManager Manager;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void onSelect() {
        Manager.CurrentGrid = this;
    }

    public void onFlood() {
        Manager.CurrentGrid = null;
    }
}
