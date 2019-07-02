// 中文测试
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject Target;

    private void Update() {
        if(Target != null) {
            transform.position = Target.transform.position;
        }
    }
}
