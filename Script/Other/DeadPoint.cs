// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPoint : MonoBehaviour
{
    public Transform Point;
    public float Radius;
    public LayerMask PlayerLayer;
    
    // Update is called once per frame
    void Update()
    {
        Collider2D[] Targets = Physics2D.OverlapCircleAll(Point.position, Radius, PlayerLayer); 
        if(Targets != null) {
            foreach(Collider2D c in Targets) {
                if(c.gameObject.GetComponent<Player>() != null) {
                    c.gameObject.GetComponent<Player>().CurrentHealth = 0;
                }
            }
        }
    }
}
