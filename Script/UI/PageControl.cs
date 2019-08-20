// 中文测试
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageControl : MonoBehaviour
{
    public GameObject LastBtn;
    public GameObject NextBtn;
    public TextMeshProUGUI Content;
    private void Awake() {
        Content.pageToDisplay = 1;
    }

    private void Update() {
        LastBtn.SetActive(Content.textInfo.pageCount > 1);
        NextBtn.SetActive(Content.textInfo.pageCount > 1);
        if(Content.textInfo.pageCount > 1) {
            if(Content.pageToDisplay == 1) {
                LastBtn.SetActive(false);
            } else if (Content.pageToDisplay == Content.textInfo.pageCount) {
                NextBtn.SetActive(false);
            }
        }
    }

    public void NextPage() {
        Content.pageToDisplay += 1;
    }

    public void LastPage() {
        Content.pageToDisplay -= 1;
    }
}
