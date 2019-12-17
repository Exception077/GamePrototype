// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FungusRebuild : MonoBehaviour
{
    public Canvas MyCavas;
    // Start is called before the first frame update
    void Start()
    {
        MyCavas.worldCamera = Camera.main;
        print("init camera");
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
