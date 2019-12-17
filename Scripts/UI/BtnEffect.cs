using UnityEngine;
using System.Collections;
using TMPro;

public class BtnEffect : MonoBehaviour {
    [SerializeField]
    Color Color0;
    [SerializeField]
    Color Color1;
    [SerializeField]
    Color Color2;
    [SerializeField]
    TextMeshProUGUI Text;
    public void setTextColor(int actionType) {
        switch (actionType) {
            case 0:
                Text.color = Color0;
                break;
            case 1:
                Text.color = Color1;
                break;
            case 2:
                Text.color = Color2;
                break;
        }
    }
}
