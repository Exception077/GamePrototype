using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameEventManager : MonoBehaviour {
    [SerializeField]
    GameObject EventContent;
    [SerializeField]
    TextMeshProUGUI Name;
    [SerializeField]
    TextMeshProUGUI Description;
    [SerializeField]
    TextMeshProUGUI TimeRemains;
    [SerializeField]
    GameEventGrid TemplateGrid;
    public GameEventGrid CurrentGrid;
    [SerializeField]
    TimeController TC;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (CurrentGrid != null) {
            showContent(CurrentGrid);
        } else {
            hideContent();
        }
    }

    public void generateGrid(GameEvent gEvent) {
        GameObject.Instantiate<GameEventGrid>(TemplateGrid, transform).GetComponent<GameEventGrid>().Event = gEvent;
    }

    public void showContent(GameEventGrid grid) {
        if (grid == null)
            return;
        EventContent.SetActive(true);
        Name.text = grid.Event.EventName;
        Description.text = grid.Event.Description;
        TimeRemains.text = grid.Event.Duration.ToString();
    }

    public void hideContent() {
        EventContent.SetActive(false);
    }
}
