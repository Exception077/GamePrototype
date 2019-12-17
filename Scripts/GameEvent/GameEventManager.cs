using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameEventManager : MonoBehaviour {
    public List<GameEvent> GameEvents = new List<GameEvent>();
    public List<GameEventGrid> EventGrids = new List<GameEventGrid>();
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
    public TimeController TC;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        foreach (GameEventGrid gameEventGrid in EventGrids) {
            if (gameEventGrid.Event.ON == false) {
                gameEventGrid.hide();
            } else {
                gameEventGrid.show();
            }
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
        GameTime time = new GameTime(grid.Event.Duration);
        TimeRemains.text = string.Format("Day{0:D2}:{1:D2}:{2:D2}", time.Days, time.Hours, time.Minutes);
    }

    public void hideContent() {
        EventContent.SetActive(false);
    }
}
