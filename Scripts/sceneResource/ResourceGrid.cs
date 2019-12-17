using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class ResourceGrid : MonoBehaviour {
    [SerializeField]
    Item Item;
    [SerializeField]
    ResourcePoint RP;
    [SerializeField]
    MouseInfo MI;
    [SerializeField]
    TextMeshProUGUI NameText;
    [SerializeField]
    Image Img;
    [SerializeField]
    Button Btn;

    bool Ready = false;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void pick() {
        RP.pick(Item);
    }

    public void initGrid(Item item, int count = 1) {
        this.Item = item;
        NameText.text = string.Format("{0}:[{1}]",this.Item.Name,this.Item.ID);
        ColorBlock cb = Btn.colors;
        switch (this.Item.RareLevel) {
            case RareLevel.NORMAL:
                Img.color = DefineData.Instance.NormalColor; 
                cb.highlightedColor = DefineData.Instance.NormalColor;
                break;
            case RareLevel.RARE:
                Img.color = DefineData.Instance.RareColor;
                cb.highlightedColor = DefineData.Instance.RareColor;
                break;
            case RareLevel.EPIC:
                Img.color = DefineData.Instance.EpicColor;
                cb.highlightedColor = DefineData.Instance.EpicColor;
                break;
            case RareLevel.LEGENDARY:
                Img.color = DefineData.Instance.LegendaryColor;
                cb.highlightedColor = DefineData.Instance.LegendaryColor;
                break;
        }
        Btn.colors = cb;
    }

    public void OnMouseOver() {
        Ready = true;
        print("over");
        if (Item == null) {
            return;
        }
        print("show");
        MI.showUI(Item.Name, Item.Description);
    }

    public void OnMouseExit() {
        print("exit");
        if (Ready == true) {
            MI.hideUI();
            Ready = false;
        }
    }
}
