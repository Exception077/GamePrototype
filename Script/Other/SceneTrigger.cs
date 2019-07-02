// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public TextMeshProUGUI Tips;
    public GameObject Point;
    public float Radius;
    public LayerMask PlayerLayer;
    public string Stage;

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(Point.transform.position, Radius, PlayerLayer)) {
            Tips.enabled = true;
            Tips.text = "Press <color=green>" + KeyCodeList.Instance.Interactive + "</color> to load scene";
            if (Input.GetKeyDown(KeyCodeList.Instance.Interactive)) {
                PlayerPrefs.SetString("NextStage", Stage);
                SceneManager.LoadScene("progress");
            }
        } else {
            Tips.enabled = false;
        }
    }
}
