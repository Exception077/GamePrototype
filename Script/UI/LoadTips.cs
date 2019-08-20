// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadTips : MonoBehaviour
{
    public TextMeshProUGUI TipsText;
    public List<string> Tips = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)Time.time);
        TipsText.text = Tips[Random.Range(0, Tips.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
