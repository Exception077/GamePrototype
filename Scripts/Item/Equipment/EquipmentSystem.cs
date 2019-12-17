using UnityEngine;
using System.Collections.Generic;

public class EquipmentSystem : MonoBehaviour {
    public List<Equipment> Equipments;
    public List<EquipmentGrid> equipmentGrids;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        foreach (Equipment equipment in Equipments) {
            if (equipment != null) {
                equipment.onEquip();
            }
        }
    }

    public Equipment find(Item item) {
        foreach (Equipment equipment in Equipments) {
            if (equipment == item) {
                return equipment;
            }
        }
        return null;
    }

    public void equip(Equipment equipment) {
        equipment.goEquip();
        Equipments.Add(equipment);
    }

    public void release(Equipment equipment) {
        equipment.onRelease();
        Equipments.Remove(equipment);
    }
}
