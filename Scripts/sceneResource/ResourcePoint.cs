using UnityEngine;
using System.Collections.Generic;
using System;

public class ResourcePoint : MonoBehaviour {
    [SerializeField]
    List<Item> Resource = new List<Item>();
    [SerializeField]
    ItemManager ItemManager;
    [SerializeField]
    ResourceGrid GridTemplate;
    [SerializeField]
    Transform Grids;
    [Header("Trigger")]
    [SerializeField]
    float Radius;
    [SerializeField]
    LayerMask TargetLayer;
    [SerializeField]
    Dialog Dialog;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Physics2D.OverlapCircle(transform.position, Radius, TargetLayer)) {
            if (Input.GetKeyDown(KeyCodeList.Instance.Crouch)) {
                Dialog.gameObject.GetComponent<Canvas>().enabled = true;
                updateGrids();
            }
        } else {
            Dialog.gameObject.GetComponent<Canvas>().enabled = false;
        }
    }

    public void pick(Item item) {
        try {
            ItemManager.addItem(item, 1);
            Resource.Remove(item);
            updateGrids();
        } catch(Exception e) {
            print(e.Message);
        }
        
    }

    public void updateGrids() {
        foreach (Transform trans in Grids) {
            Destroy(trans.gameObject);
        }
        foreach (Item item in Resource) {
            GameObject.Instantiate<ResourceGrid>(GridTemplate, Grids).initGrid(item);
        }
    }
}
