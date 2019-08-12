using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Impression {
    public string Name;
    public int Process;
}
public class Relation : MonoBehaviour {
    [SerializeField]
    readonly List<Impression> Impressions = new List<Impression>();

    /// <summary>
    /// 添加新的角色好感信息
    /// </summary>
    /// <param name="name">角色名</param>
    /// <param name="process">好感度</param>
    public void addImpression(string name, int process) {
        Impression impression = new Impression();
        impression.Name = name;
        impression.Process = process;
        Impressions.Add(impression);
    }

    /// <summary>
    /// 查询好感度
    /// </summary>
    /// <param name="name">角色名</param>
    /// <returns></returns>
    public int getImpression(string name) {
        int process = 0;
        foreach (Impression i in Impressions) {
            if (i.Name == name) {
                process = i.Process;
            }
        }
        return process;
    }

    /// <summary>
    /// 修改好感度
    /// </summary>
    /// <param name="name">角色名</param>
    /// <param name="process">好感</param>
    public void setImpression(string name, int process) {
        Impressions.ForEach(i =>
        {
            if (i.Name == name) {
                i.Process = process;
            }
        });
    }
}
