// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ConversationTrigger : MonoBehaviour
{
    public LayerMask TargetLayer;
    public Collider2D Range;
    public string MessageName;
    public GameObject GO;
    [SerializeField]
    GameCharacter Character;
    [SerializeField]
    UIManager UIControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapArea(Range.bounds.min, Range.bounds.max, TargetLayer)) {
            GO.SetActive(true);
            if (Input.GetKeyDown(KeyCodeList.Instance.Interactive)) {
                Flowchart.BroadcastFungusMessage(MessageName);
            }
            if (Input.GetKeyDown(KeyCode.Q)) {
                showItemSlot();
            }
        } else {
            GO.SetActive(false);
        }
    }

    public void showItemSlot() {
        Character.Bag.Owner = Character;
        UIControl.openDialog(UIControl.findDialog("NPCBag"));
        // 初始化背包
        Character.Bag.clearItem();
        // 刷新背包
        foreach(ItemLoadIndex i in Character.ItemReference) {
            Character.Bag.addItem(ItemStock.Instance.getItemByID(i.ID), i.Count);
        }
    }
}
