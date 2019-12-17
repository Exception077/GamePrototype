using UnityEngine;
using System.Collections;

public class EquipmentDialog : Dialog {
    [SerializeField]
    GameObject GridComponents;

    private void Start() {
        if (PlayerPrefs.HasKey("DialogPosX" + DialogName)) {
            transform.localPosition = new Vector3(PlayerPrefs.GetFloat("DialogPosX" + DialogName), PlayerPrefs.GetFloat("DialogPosY" + DialogName), PlayerPrefs.GetFloat("DialogPosZ" + DialogName));
        }
    }

    private void Update() {
        PlayerPrefs.SetFloat("DialogPosX" + DialogName, transform.localPosition.x);
        PlayerPrefs.SetFloat("DialogPosY" + DialogName, transform.localPosition.y);
        PlayerPrefs.SetFloat("DialogPosZ" + DialogName, transform.localPosition.z);
    }

    public override void closeDialog() {
        gameObject.GetComponent<Canvas>().enabled = false;
        GridComponents.SetActive(false);
    }

    public override void openDialog() {
        if (gameObject.GetComponent<Canvas>().enabled == true) {
            // 若已经打开背包，则将其关闭
            closeDialog();
        }
        else {
            // 打开背包
            gameObject.GetComponent<Canvas>().enabled = true;
            GridComponents.SetActive(true);
        }
    }
}
