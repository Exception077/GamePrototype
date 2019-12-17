using UnityEngine;
using System.Collections;

public class DefineData {
    private static DefineData MyInstance;

    public Color32 NormalColor = new Color32(169, 169, 169, 255);// #A9A9A9
    public Color32 RareColor = new Color32(65, 105, 225, 255);// #4169E1
    public Color32 EpicColor = new Color32(186, 85, 211, 255);// #BA55D3
    public Color32 LegendaryColor = new Color32(255,215,0,255);// #FFD700

    public static DefineData Instance {
        get {
            if (MyInstance == null) {
                MyInstance = new DefineData();
            }
            return MyInstance;
        }
    }
}
